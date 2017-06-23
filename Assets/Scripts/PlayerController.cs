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
	private SpriteRenderer renderer;
	private Animation anim;

	private float moveHDelay = 0;
	private float moveVDelay = 0;

	void Start () {
		rd2d = GetComponent<Rigidbody2D>();
		renderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animation> ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.R) && !respawn && !dead) {
			respawn = true;
			dead = true;
		}
	}
	
	void FixedUpdate () {
		if(!dead && !finish)
			MovePlayer ();
	}

	void MovePlayer(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		float speedRate = Mathf.Max (Mathf.Abs (moveVertical), Mathf.Abs (moveHorizontal))/decceleration;
		float rot = Mathf.Rad2Deg * Mathf.Atan2 (-moveHorizontal, moveVertical);
		if (Mathf.Abs (moveHorizontal) == 1 || Mathf.Abs (moveHorizontal) - Mathf.Abs (moveHDelay) > 0 || Mathf.Abs (moveVertical) == 1 || Mathf.Abs (moveVertical) - Mathf.Abs (moveVDelay) > 0) {
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
			if (!CompareColor (renderer.color, coll.gameObject.GetComponent<SpriteRenderer> ().color)) {
				dead = true;
				obstacleKill = true;
				StartCoroutine (Death());
			}
				
		}
		if (coll.gameObject.CompareTag ("LevelFinish")) {
			if (!finish) {
				GameObject timerCanvas = GameObject.Find ("TimerCanvas");
				if (timerCanvas != null) {
					SaveLoad.SaveTimer (timerCanvas.GetComponent<TimerScript> ().getTimer ());
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
		GameObject levelFinish = GameObject.Find ("LevelFinish");
		finish = false;
		SceneManager.LoadScene (levelFinish.gameObject.GetComponent<LevelFinish> ().nextLevel, LoadSceneMode.Single);
		Cursor.visible = true;
	}

	void OnTriggerStay2D(Collider2D coll){
		if(coll.gameObject.CompareTag("Colorer")){
			renderer.color=coll.gameObject.GetComponent<SpriteRenderer>().color;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Obstacles"))
			obstacleCollide = false;
	}

	bool CompareColor(Color color1,Color color2){
		if (color1.r == color2.r && color1.g == color2.g && color1.b == color2.b && color1.a == color2.a)
			return true;
		else
			return false;
	}
}
