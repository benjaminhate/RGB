using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float decceleration;

	public bool dead = false;
	public bool respawn = false;
	public bool finish = false;
	public bool obstacleCollide = false;
	public bool obstacleKill = false;
	public string obstacle;

	private Rigidbody2D rd2d;
	private Animation anim;

	private float moveHDelay = 0;
	private float moveVDelay = 0;

    private Joystick joystick;

	void Start () {
#if UNITY_ANDROID
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponentInChildren<Joystick>();
#endif
		rd2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animation> ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.R)) {
			Refresh ();
		}
        if (Input.GetKeyDown(KeyCode.F))
        {
            MapSaveLoad.SaveMapFromScene();
        }
	}
	
	void FixedUpdate () {
		if(IsMoving())
			MovePlayer ();
	}

	public void Refresh(){
		if (!respawn && !dead) {
			respawn = true;
			dead = true;
            GameObject timerCanvas = GameObject.Find("TimerCanvas");
            if (timerCanvas) {
                timerCanvas.GetComponent<TimerScript>().ResetTimer();
            }
		}
	}

    public bool IsMoving()
    {
        return !dead && !finish;
    }

	void MovePlayer(){
        float moveHorizontal = 0;
        float moveVertical = 0;
#if UNITY_ANDROID
        Vector2 dir = joystick.GetDirection();
        dir *= 1.5f;
        if (Mathf.Abs(dir.x) > 1f) dir.x = Mathf.Sign(dir.x) * 1f;
        if (Mathf.Abs(dir.y) > 1f) dir.y = Mathf.Sign(dir.y) * 1f;
        Debug.Log(dir);
        moveHorizontal = dir.x;
        moveVertical = dir.y;
#else
        moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");
        Debug.Log(new Vector2(moveHorizontal,moveVertical));
#endif
        float speedRate = Mathf.Max (Mathf.Abs (moveVertical), Mathf.Abs (moveHorizontal))/decceleration;
		float rot = Mathf.Rad2Deg * Mathf.Atan2 (-moveHorizontal, moveVertical);
        if ((Mathf.Abs (moveHorizontal) != 0 && Mathf.Abs (moveHorizontal) - Mathf.Abs (moveHDelay) >= 0 )
            || (Mathf.Abs (moveVertical) != 0 & Mathf.Abs (moveVertical) - Mathf.Abs (moveVDelay) >= 0)) {
			rd2d.rotation = rot;
			speedRate *= decceleration;
		}
        moveHDelay = moveHorizontal;
		moveVDelay = moveVertical;
		transform.Translate (speed * speedRate * Vector3.up * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if(coll.gameObject.CompareTag("Obstacles") && !finish && !dead)
		{
			obstacleCollide = true;
			obstacle = coll.gameObject.name;
            if (!GetComponent<ColorElement>().SameColor(coll.GetComponent<ColorElement>())) {
				dead = true;
				obstacleKill = true;
				StartCoroutine (Death());
			}
				
		}
		if (coll.gameObject.CompareTag ("LevelFinish")) {
			if (!finish) {
                GameObject timerCanvas = GameObject.Find("TimerCanvas");
                if (timerCanvas != null) {
                    Debug.Log("Saving Timer");
					SaveLoad.SaveTimer (timerCanvas.GetComponent<TimerScript> ().GetTimer ()
                        ,GameObject.FindGameObjectWithTag("MapCreator").GetComponent<MapCreator>().GetMapData().GetLevelName());
				}
				StartCoroutine (Finish ());
			}
		}

		if (coll.gameObject.CompareTag ("TutoTransition")) {
			GameObject tuto = GameObject.Find ("TutoCanvas");
			if (tuto != null) {
				tuto.GetComponent<TutoScript> ().transObjective.setDone (true);
			}
		}
	}

	private IEnumerator RotateAnimation(){
		do {
			transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, new Vector3(0,0,180), Time.deltaTime*10);
			yield return null;
		} while(Vector3.Distance (transform.eulerAngles, new Vector3(0,0,180)) > 1f);
	}

	private IEnumerator WaitForAnimation ( Animation animation )
	{
		do
		{
			yield return null;
		} while ( animation.isPlaying );
	}

	IEnumerator Death(){
		yield return StartCoroutine (RotateAnimation ());
		anim.PlayQueued ("DeathAnimation");
		yield return StartCoroutine (WaitForAnimation (anim));
		obstacleKill = false;
		respawn = true;
	}

	IEnumerator Finish(){
		finish = true;
		yield return StartCoroutine (RotateAnimation ());
		anim.PlayQueued ("FinishAnimation");
		yield return StartCoroutine (WaitForAnimation (anim));
		GameObject levelFinish = GameObject.FindGameObjectWithTag ("LevelFinish");
		finish = false;
        if (levelFinish.GetComponent<LevelFinish>())
        {
            GameObject.FindGameObjectWithTag("MapCreator").GetComponent<MapCreator>()
                .ChangeLevel(levelFinish.GetComponent<LevelFinish>().nextLevel);
            //SceneManager.LoadScene (levelFinish.GetComponent<LevelFinish> ().nextLevel, LoadSceneMode.Single);
            Cursor.visible = true;
        }
        if (levelFinish.GetComponent<EndTutorial>())
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }
	}

	void OnTriggerStay2D(Collider2D coll){
		if(coll.gameObject.CompareTag("Colorer")){
            GetComponent<ColorElement>().ChangeColor(coll.GetComponent<ColorElement>().GetColor());
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Obstacles"))
			obstacleCollide = false;
	}
}
