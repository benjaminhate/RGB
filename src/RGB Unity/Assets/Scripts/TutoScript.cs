using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objective {

    protected LanguageController lang;

    public void Init()
    {
        lang = new LanguageController(SaveLoad.Load().GetLanguage());
    }

    public virtual string GetDescription (){
        return "";
	}

	public virtual bool IsCompleted (){
		return false;
	}

	public virtual void Check (){
	}
}

public class TransitionObjective : Objective {
	private bool _done;

	public void SetDone(bool doneChange){
		_done = doneChange;
	}

	public override string GetDescription(){
        Init();
        return lang.GetString("tutoTransition");
	}

	public override bool IsCompleted(){
		return _done;
	}
}

public class MoveObjective : Objective {

	private bool _up;
	private bool _down;
	private bool _left;
	private bool _right;

	public void UpPressed(){
		_up = true;
	}
	public void DownPressed(){
		_down = true;
	}
	public void LeftPressed(){
		_left = true;
	}
	public void RightPressed(){
		_right = true;
	}

	public override string GetDescription(){
        Init();
        var upColor = _up ? "green" : "red";
		var downColor = _down ? "green" : "red";
		var rightColor = _right ? "green" : "red";
		var leftColor = _left ? "green" : "red";
        return lang.GetString("tutoMove") + "\n" +
            "<color=" + upColor + ">" + lang.GetString("tutoMoveUp") + "</color>\n" +
            "<color=" + downColor + ">" + lang.GetString("tutoMoveDown") + "</color>\n" +
            "<color=" + rightColor + ">" + lang.GetString("tutoMoveRight") + "</color>\n" +
            "<color=" + leftColor + ">" + lang.GetString("tutoMoveLeft") + "</color>";
	}

	public override bool IsCompleted(){
		return (_up && _down && _left && _right);
	}

	public override void Check(){
		var moveHorizontal = Input.GetAxis("Horizontal");
		var moveVertical = Input.GetAxis("Vertical");
		var floor = 0.15f;
		var player = GameObject.Find("Player").GetComponent<PlayerController>();
		if (moveVertical > floor && !player.dead) {
			UpPressed ();
		}
		if (moveVertical < -floor && !player.dead) {
			DownPressed ();
		}
		if (moveHorizontal > floor && !player.dead) {
			RightPressed ();
		}
		if (moveHorizontal < -floor && !player.dead) {
			LeftPressed ();
		}
	}
}

public class DiagObjective : Objective {

	private bool _upRight;
	private bool _downLeft;

	public void UpRightPressed(){
		_upRight = true;
	}
	public void DownLeftPressed(){
		_downLeft = true;
	}

	public override string GetDescription(){
        Init();
        var upRightColor = _upRight ? "green" : "red";
		var downLeftColor = _downLeft ? "green" : "red";
        return lang.GetString("tutoDiagonal") + "\n" +
        "<color=" + upRightColor + ">" + lang.GetString("tutoDiagonalUpRight") + "</color>\n" +
        "<color=" + downLeftColor + ">" + lang.GetString("tutoDiagonalDownLeft") + "</color>\n";
	}

	public override bool IsCompleted(){
		return (_upRight && _downLeft);
	}

	public override void Check(){
		var start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=7.25f;
		start.startY=9.5f;
		start.startRot = 180;
		var moveHorizontal = Input.GetAxis("Horizontal");
		var moveVertical = Input.GetAxis("Vertical");
		var floor = 0.3f;
		var player = GameObject.Find("Player").GetComponent<PlayerController>();
		if (moveVertical > floor && moveHorizontal > floor && !player.dead) {
			UpRightPressed ();
		}
		if (moveVertical < -floor && moveHorizontal < -floor && !player.dead) {
			DownLeftPressed ();
		}
	}
}

public class RayObjective : Objective {
	private bool _rayDead;

	public void RayDead(){
		_rayDead = true;
	}

	public override string GetDescription(){
        Init();
        var deadColor = _rayDead ? "green" : "red";
        return lang.GetString("tutoRay") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoRayDeath") + "</color>";
	}

	public override bool IsCompleted(){
		return _rayDead;
	}

	public override void Check(){
		var start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=7.25f;
		start.startY=-1.5f;
		start.startRot = 180;
		var player = GameObject.Find("Player").GetComponent<PlayerController>();
		if (player.obstacleKill && player.obstacle.Contains("Ray"))
			RayDead ();
	}
}

public class DetectorObjective : Objective {
	private bool _detectorDead;

	public void DetectorDead(){
		_detectorDead = true;
	}

	public override string GetDescription(){
        Init();
        var deadColor = _detectorDead ? "green" : "red";
        return lang.GetString("tutoDetector") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoDetectorDeath") + "</color>";
	}

	public override bool IsCompleted(){
		return _detectorDead;
	}

	public override void Check (){
		var start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=7.25f;
		start.startY=-11.5f;
		start.startRot = 180;
		var player = GameObject.Find("Player").GetComponent<PlayerController>();
		if (player.obstacleKill && player.obstacle.Contains("Detector"))
			DetectorDead ();
	}
}

public class CameraObjective : Objective {
	private bool _cameraDead;

	public void CameraDead(){
		_cameraDead = true;
	}

	public override string GetDescription(){
        Init();
        var deadColor = _cameraDead ? "green" : "red";
        return lang.GetString("tutoCamera") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoCameraDeath") + "</color>";
	}

	public override bool IsCompleted(){
		return _cameraDead;
	}

	public override void Check (){
		var start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-2f;
		start.startY=-16f;
		start.startRot = 90;
		var player = GameObject.Find("Player").GetComponent<PlayerController>();
		if (player.obstacleKill && player.obstacle.Contains("Camera"))
			CameraDead ();
	}
}

public class RespawnObjective : Objective {
	private bool _key;

	public void KeyPressed(){
		_key = true;
	}

	public override string GetDescription (){
        Init();
        var keyColor = _key ? "green" : "red";
        return lang.GetString("tutoRespawn") + "\n" +
            "<color=" + keyColor + ">" + lang.GetString("tutoRespawnTouch") + "</color>";
	}

	public override bool IsCompleted (){
		return _key;
	}

	public override void Check (){
		var start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-7.25f;
		start.startY=-8.5f;
		start.startRot = 0;
		if (Input.GetKeyDown (KeyCode.R))
			KeyPressed ();
	}
}

public class ColorerObjective : Objective {
	private bool _red;
	private bool _green;
	private bool _blue;
	private bool _init;

	private Color _redC = new Color (1f, 0.2f, 0.2f, 1f);
	private Color _greenC = new Color(0f,0.8f,0f,1f);
	private Color _blueC = new Color (0.1f, 0.3f, 1f, 1f);

	public void RedChange(){
		_red = true;
	}

	public void BlueChange(){
		_blue = true;
	}

	public void GreenChange(){
		_green = true;
	}

	private bool CompareColor(Color color1,Color color2,bool init)
	{
		return color1 == color2 && init;
	}

	public override string GetDescription (){
        Init();
        var redColor = _red ? "green" : "red";
		var greenColor = _green ? "green" : "red";
		var blueColor = _blue ? "green" : "red";
        return lang.GetString("tutoColorer") + "\n" +
            "<color=" + greenColor + ">" + lang.GetString("tutoColorerGreen") + "</color>\n" +
            "<color=" + blueColor + ">" + lang.GetString("tutoColorerBlue") + "</color>\n" +
            "<color=" + redColor + ">" + lang.GetString("tutoColorerRed") + "</color>\n";
	}

	public override bool IsCompleted (){
		return (_red && _blue && _green);
	}

	public override void Check (){
		var start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-7.25f;
		start.startY=1.5f;
		start.startRot = 0;
		var playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
		if (CompareColor (playerSpriteRenderer.color, _redC,_init))
			_red = true;
		if (CompareColor (playerSpriteRenderer.color, _greenC, true)) {
			_green = true;
			_init = true;
		}
		if (CompareColor (playerSpriteRenderer.color, _blueC, true)) {
			_blue = true;
			_init = true;
		}
	}
}

public class FinishObjective : Objective {
	public override void Check (){
		var start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-7.25f;
		start.startY=11.5f;
		start.startRot = 0;
	}
	public override string GetDescription (){
        Init();
        return lang.GetString("tutoEnd");
	}
}

public class TutoScript : MonoBehaviour {
	public GameObject moveWall;
	public GameObject diagWall;
	public GameObject rayWall;
	public GameObject detectorWall;
	public GameObject cameraWall;
	public GameObject respawnWall;
	public GameObject colorerWall;

	public GameObject moveTransition;
	public GameObject diagTransition;
	public GameObject rayTransition;
	public GameObject detectorTransition;
	public GameObject cameraTransition;
	public GameObject respawnTransition;
	public GameObject colorerTransition;

	public TextMeshProUGUI tutoText;
    public Image quitTuto;

	public TransitionObjective transObjective=new TransitionObjective();
	private bool _transition;

	private MoveObjective _moveObjective=new MoveObjective();
	private DiagObjective _diagObjective=new DiagObjective();
	private RayObjective _rayObjective=new RayObjective();
	private DetectorObjective _detectorObjective = new DetectorObjective();
	private CameraObjective _cameraObjective = new CameraObjective();
	private RespawnObjective _respawnObjective = new RespawnObjective();
	private ColorerObjective _colorerObjective = new ColorerObjective();
	private FinishObjective _finishObjective = new FinishObjective ();

	private List<Objective> _objectiveList = new List<Objective>();
	private List<GameObject> _wallList = new List<GameObject> ();
	private List<GameObject> _transitionList = new List<GameObject> ();
	private int _active;
	private int _maxActive;

    private LanguageController _lang;
    private GameObject _quitText;
    private GameObject _quitConfirm;
    private GameObject _quitCancel;

    private void Awake()
    {
        _lang = new LanguageController(SaveLoad.Load().GetLanguage());
        _quitText = GameObject.Find("QuitText");
        _quitConfirm = GameObject.Find("QuitConfirm");
        _quitCancel = GameObject.Find("QuitCancel");
    }

    private void Start(){
		_objectiveList.Add (_moveObjective);
		_wallList.Add (moveWall);
		_transitionList.Add (moveTransition);
		_objectiveList.Add (_diagObjective);
		_wallList.Add (diagWall);
		_transitionList.Add (diagTransition);
		_objectiveList.Add (_rayObjective);
		_wallList.Add (rayWall);
		_transitionList.Add (rayTransition);
		_objectiveList.Add (_detectorObjective);
		_wallList.Add (detectorWall);
		_transitionList.Add (detectorTransition);
		_objectiveList.Add (_cameraObjective);
		_wallList.Add (cameraWall);
		_transitionList.Add (cameraTransition);
		_objectiveList.Add (_respawnObjective);
		_wallList.Add (respawnWall);
		_transitionList.Add (respawnTransition);
		_objectiveList.Add (_colorerObjective);
		_wallList.Add (colorerWall);
		_transitionList.Add (colorerTransition);
		_objectiveList.Add (_finishObjective);
		_maxActive = _objectiveList.Count-1;

        Resume();
	}

    private void Update(){
        UpdateText();
		if (transObjective.IsCompleted ()) {
			_transition = false;
			transObjective.SetDone (false);
			_transitionList [_active-1].gameObject.SetActive (false);
		}
		if (!_transition)
			_objectiveList [_active].Check ();
		if (_objectiveList [_active].IsCompleted () && !_transition) {
			_wallList [_active].gameObject.SetActive (false);
			_transition = true;
			if (_active < _maxActive)
				_active++;
		}
		tutoText.text = _transition ? transObjective.GetDescription () : _objectiveList [_active].GetDescription ();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitTuto();
        }
	}

    private void UpdateText()
    {
        _quitText.GetComponent<TextMeshProUGUI>().text = _lang.GetString("tutoQuit");
        _quitConfirm.GetComponentInChildren<TextMeshProUGUI>().text = _lang.GetString("yes");
        _quitCancel.GetComponentInChildren<TextMeshProUGUI>().text = _lang.GetString("no");
    }

    private void QuitTuto()
    {
        quitTuto.gameObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        quitTuto.gameObject.SetActive(false);
        Cursor.visible = false;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.CompareTag("Player"))
	    {
		    transObjective.SetDone (true);
	    }
    }
}