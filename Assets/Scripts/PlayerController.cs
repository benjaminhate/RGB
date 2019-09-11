using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float decceleration;

	public bool dead;
	public bool respawn;
	public bool finish;
	public bool obstacleCollide;
	public bool obstacleKill;
	public string obstacle;

	private Rigidbody2D rd2d;
	private Animation anim;

	private float moveHDelay;
	private float moveVDelay;

    private Joystick joystick;

    private void Start () {
#if UNITY_ANDROID
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponentInChildren<Joystick>();
#endif
		rd2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animation> ();
	}

    private void Update() {
		if (Input.GetKeyDown (KeyCode.R)) {
			Refresh ();
		}
        if (Input.GetKeyDown(KeyCode.F))
        {
            MapSaveLoad.SaveMapFromScene();
        }
	}

    private void FixedUpdate () {
		if(IsMoving())
			MovePlayer ();
	}

	public void Refresh(){
		if (respawn || dead) return;
		
		respawn = true;
		dead = true;
		var timerCanvas = GameObject.Find("TimerCanvas");
		if (timerCanvas) {
			timerCanvas.GetComponent<TimerScript>().ResetTimer();
		}
	}

    public bool IsMoving()
    {
        return !dead && !finish;
    }

    private void MovePlayer(){
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
//        Debug.Log(new Vector2(moveHorizontal,moveVertical));
#endif
        var speedRate = Mathf.Max (Mathf.Abs (moveVertical), Mathf.Abs (moveHorizontal))/decceleration;
		var rot = Mathf.Rad2Deg * Mathf.Atan2 (-moveHorizontal, moveVertical);
        if ((Mathf.Abs (moveHorizontal) != 0 && Mathf.Abs (moveHorizontal) - Mathf.Abs (moveHDelay) >= 0 )
            || (Mathf.Abs (moveVertical) != 0 & Mathf.Abs (moveVertical) - Mathf.Abs (moveVDelay) >= 0)) {
			rd2d.rotation = rot;
			speedRate *= decceleration;
		}
        moveHDelay = moveHorizontal;
		moveVDelay = moveVertical;
		transform.Translate (speed * speedRate * Time.deltaTime * Vector3.up);
	}

    private void OnTriggerEnter2D(Collider2D coll) 
	{
		if(coll.gameObject.CompareTag("Obstacles") && !finish && !dead)
		{
			obstacleCollide = true;
			obstacle = coll.gameObject.name;
            if (!GetComponent<ColorElement>().SameColor(coll.GetComponent<ColorElement>().colorSo)) {
				dead = true;
				obstacleKill = true;
				StartCoroutine (Death());
			}
				
		}
		if (coll.gameObject.CompareTag ("LevelFinish")) {
			if (!finish) {
                var timerCanvas = GameObject.Find("TimerCanvas");
                if (timerCanvas != null) {
                    Debug.Log("Saving Timer");
					SaveLoad.SaveTimer (timerCanvas.GetComponent<TimerScript> ().GetTimer ()
                        ,GameObject.FindGameObjectWithTag("MapCreator").GetComponent<MapCreator>().GetMapData().GetLevelName());
				}
				StartCoroutine (Finish ());
			}
		}

		if (coll.gameObject.CompareTag ("TutoTransition")) {
			var tuto = GameObject.Find ("TutoCanvas");
			if (tuto != null) {
				tuto.GetComponent<TutoScript> ().transObjective.SetDone (true);
			}
		}
	}

	private IEnumerator RotateAnimation(){
		do {
			transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, new Vector3(0,0,180), Time.deltaTime*10);
			yield return null;
		} while(Vector3.Distance (transform.eulerAngles, new Vector3(0,0,180)) > 1f);
	}

	private IEnumerator WaitForAnimation ( Animation animationPlayed )
	{
		do
		{
			yield return null;
		} while ( animationPlayed.isPlaying );
	}

	private IEnumerator Death(){
		yield return StartCoroutine (RotateAnimation ());
		anim.PlayQueued ("DeathAnimation");
		yield return StartCoroutine (WaitForAnimation (anim));
		obstacleKill = false;
		respawn = true;
	}

	private IEnumerator Finish(){
		finish = true;
		yield return StartCoroutine (RotateAnimation ());
		anim.PlayQueued ("FinishAnimation");
		yield return StartCoroutine (WaitForAnimation (anim));
		var levelFinish = GameObject.FindGameObjectWithTag ("LevelFinish");
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

	private void OnTriggerStay2D(Collider2D coll){
		if(coll.gameObject.CompareTag("Colorer")){
            GetComponent<ColorElement>().ChangeColor(coll.GetComponent<ColorElement>().Color);
		}
	}

	private void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Obstacles"))
			obstacleCollide = false;
	}
}
