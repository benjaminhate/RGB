using System;
using System.Collections;
using Joystick_Pack.Scripts.Base;
using Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent((typeof(Animator)))]
public class PlayerController : MonoBehaviour {

	public float speed;
	public float decceleration;

	public bool dead;
	public bool respawn;
	public bool finish;
	public bool obstacleCollide;
	public bool obstacleKill;
	public string obstacle;

	public LevelFinish levelFinish;
	public LevelStart levelStart;
	
	private Rigidbody2D _rd2d;
	public Animator Animator { get; private set; }

	private float _moveHDelay;
	private float _moveVDelay;

    private Joystick _joystick;
    private static readonly int IsFinishAnimator = Animator.StringToHash("IsFinish");
    private static readonly int IsDeadAnimator = Animator.StringToHash("IsDead");
    private static readonly int FaceYAnimator = Animator.StringToHash("FaceY");
    private static readonly int FaceXAnimator = Animator.StringToHash("FaceX");

    public bool IsMoving => !dead && !finish;

    private void Start () {
#if UNITY_ANDROID
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponentInChildren<Joystick>();
#endif
		_rd2d = GetComponent<Rigidbody2D>();
		Animator = GetComponent<Animator> ();
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
		if(IsMoving)
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

    private void MovePlayer(){
        float moveHorizontal = 0;
        float moveVertical = 0;
#if UNITY_ANDROID
        var dir = joystick.Direction * 1.5f;
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
	    var horizontalNotNull = Math.Abs(moveHorizontal) > 0.000001;
	    var verticalNotNull = Mathf.Abs(moveVertical) > 0.000001;
	    
        var speedRate = Mathf.Max (Mathf.Abs (moveVertical), Mathf.Abs (moveHorizontal))/decceleration;
		var rot = Mathf.Rad2Deg * Mathf.Atan2 (-moveHorizontal, moveVertical);
        if ((horizontalNotNull && Mathf.Abs (moveHorizontal) - Mathf.Abs (_moveHDelay) >= 0 )
            || (verticalNotNull & Mathf.Abs (moveVertical) - Mathf.Abs (_moveVDelay) >= 0)) {
			_rd2d.SetRotation(rot);
			speedRate *= decceleration;
		}
        _moveHDelay = moveHorizontal;
		_moveVDelay = moveVertical;
		transform.Translate (speed * speedRate * Time.deltaTime * Vector3.up);
		if (Math.Abs(moveHorizontal) > 0.1 || Mathf.Abs(moveVertical) > 0.1)
		{
			Animator.SetFloat(FaceXAnimator, moveHorizontal);
			Animator.SetFloat(FaceYAnimator, moveVertical);
		}
	}

    public void Dead()
    {
	    dead = true;
	    obstacleKill = true;
	    Death();
    }
	
	public void Finish(){
		finish = true;
		Animator.SetBool(IsFinishAnimator, true);
	}

	public void OnFinishAnimationEnd()
	{
		Animator.SetBool(IsFinishAnimator, false);
		if(levelFinish != null) levelFinish.ChangeLevel();
		
		finish = false;
		
		if (GetComponent<EndTutorial>())
		{
			SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
		}
	}

	private void Death(){
		Animator.SetBool(IsDeadAnimator, true);
	}

	public void OnDeathAnimationEnd()
	{
		Animator.SetBool(IsDeadAnimator, false);
		obstacleKill = false;
        respawn = true;
	}

	private void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Obstacles"))
			obstacleCollide = false;
	}

	public void OnRespawnAnimationEnd()
	{
		if(levelStart != null) levelStart.OnPlayerRespawnAnimationEnd();
	}
}
