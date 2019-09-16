using System;
using System.Collections.Generic;
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
	private bool done;

	public void SetDone(bool doneChange){
		done = doneChange;
	}

	public override string GetDescription(){
        Init();
        return lang.GetString("tutoTransition");
	}

	public override bool IsCompleted(){
		return done;
	}
}

public class MoveObjective : Objective {

	private bool up;
	private bool down;
	private bool left;
	private bool right;

	public void UpPressed(){
		up = true;
	}
	public void DownPressed(){
		down = true;
	}
	public void LeftPressed(){
		left = true;
	}
	public void RightPressed(){
		right = true;
	}

	public override string GetDescription(){
        Init();
        var upColor = up ? "green" : "red";
		var downColor = down ? "green" : "red";
		var rightColor = right ? "green" : "red";
		var leftColor = left ? "green" : "red";
        return lang.GetString("tutoMove") + "\n" +
            "<color=" + upColor + ">" + lang.GetString("tutoMoveUp") + "</color>\n" +
            "<color=" + downColor + ">" + lang.GetString("tutoMoveDown") + "</color>\n" +
            "<color=" + rightColor + ">" + lang.GetString("tutoMoveRight") + "</color>\n" +
            "<color=" + leftColor + ">" + lang.GetString("tutoMoveLeft") + "</color>";
	}

	public override bool IsCompleted(){
		return (up && down && left && right);
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

	private bool upRight;
	private bool downLeft;

	public void UpRightPressed(){
		upRight = true;
	}
	public void DownLeftPressed(){
		downLeft = true;
	}

	public override string GetDescription(){
        Init();
        var upRightColor = upRight ? "green" : "red";
		var downLeftColor = downLeft ? "green" : "red";
        return lang.GetString("tutoDiagonal") + "\n" +
        "<color=" + upRightColor + ">" + lang.GetString("tutoDiagonalUpRight") + "</color>\n" +
        "<color=" + downLeftColor + ">" + lang.GetString("tutoDiagonalDownLeft") + "</color>\n";
	}

	public override bool IsCompleted(){
		return (upRight && downLeft);
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
	private bool rayDead;

	public void RayDead(){
		rayDead = true;
	}

	public override string GetDescription(){
        Init();
        var deadColor = rayDead ? "green" : "red";
        return lang.GetString("tutoRay") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoRayDeath") + "</color>";
	}

	public override bool IsCompleted(){
		return rayDead;
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
	private bool detectorDead;

	public void DetectorDead(){
		detectorDead = true;
	}

	public override string GetDescription(){
        Init();
        var deadColor = detectorDead ? "green" : "red";
        return lang.GetString("tutoDetector") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoDetectorDeath") + "</color>";
	}

	public override bool IsCompleted(){
		return detectorDead;
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
	private bool cameraDead;

	public void CameraDead(){
		cameraDead = true;
	}

	public override string GetDescription(){
        Init();
        var deadColor = cameraDead ? "green" : "red";
        return lang.GetString("tutoCamera") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoCameraDeath") + "</color>";
	}

	public override bool IsCompleted(){
		return cameraDead;
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
	private bool key;

	public void KeyPressed(){
		key = true;
	}

	public override string GetDescription (){
        Init();
        var keyColor = key ? "green" : "red";
        return lang.GetString("tutoRespawn") + "\n" +
            "<color=" + keyColor + ">" + lang.GetString("tutoRespawnTouch") + "</color>";
	}

	public override bool IsCompleted (){
		return key;
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
	private bool red;
	private bool green;
	private bool blue;
	private bool init;

	private Color redC = new Color (1f, 0.2f, 0.2f, 1f);
	private Color greenC = new Color(0f,0.8f,0f,1f);
	private Color blueC = new Color (0.1f, 0.3f, 1f, 1f);

	public void RedChange(){
		red = true;
	}

	public void BlueChange(){
		blue = true;
	}

	public void GreenChange(){
		green = true;
	}

	private bool CompareColor(Color color1,Color color2,bool init)
	{
		return color1 == color2 && init;
	}

	public override string GetDescription (){
        Init();
        var redColor = red ? "green" : "red";
		var greenColor = green ? "green" : "red";
		var blueColor = blue ? "green" : "red";
        return lang.GetString("tutoColorer") + "\n" +
            "<color=" + greenColor + ">" + lang.GetString("tutoColorerGreen") + "</color>\n" +
            "<color=" + blueColor + ">" + lang.GetString("tutoColorerBlue") + "</color>\n" +
            "<color=" + redColor + ">" + lang.GetString("tutoColorerRed") + "</color>\n";
	}

	public override bool IsCompleted (){
		return (red && blue && green);
	}

	public override void Check (){
		var start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-7.25f;
		start.startY=1.5f;
		start.startRot = 0;
		var playerSpriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
		if (CompareColor (playerSpriteRenderer.color, redC,init))
			red = true;
		if (CompareColor (playerSpriteRenderer.color, greenC, true)) {
			green = true;
			init = true;
		}
		if (CompareColor (playerSpriteRenderer.color, blueC, true)) {
			blue = true;
			init = true;
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

	public Text tutoText;
    public Image quitTuto;

	public TransitionObjective transObjective=new TransitionObjective();
	private bool transition;

	private MoveObjective moveObjective=new MoveObjective();
	private DiagObjective diagObjective=new DiagObjective();
	private RayObjective rayObjective=new RayObjective();
	private DetectorObjective detectorObjective = new DetectorObjective();
	private CameraObjective cameraObjective = new CameraObjective();
	private RespawnObjective respawnObjective = new RespawnObjective();
	private ColorerObjective colorerObjective = new ColorerObjective();
	private FinishObjective finishObjective = new FinishObjective ();

	private List<Objective> objectiveList = new List<Objective>();
	private List<GameObject> wallList = new List<GameObject> ();
	private List<GameObject> transitionList = new List<GameObject> ();
	private int active;
	private int maxActive;

    private LanguageController lang;
    private GameObject quitText;
    private GameObject quitConfirm;
    private GameObject quitCancel;

    private void Awake()
    {
        lang = new LanguageController(SaveLoad.Load().GetLanguage());
        quitText = GameObject.Find("QuitText");
        quitConfirm = GameObject.Find("QuitConfirm");
        quitCancel = GameObject.Find("QuitCancel");
    }

    private void Start(){
		objectiveList.Add (moveObjective);
		wallList.Add (moveWall);
		transitionList.Add (moveTransition);
		objectiveList.Add (diagObjective);
		wallList.Add (diagWall);
		transitionList.Add (diagTransition);
		objectiveList.Add (rayObjective);
		wallList.Add (rayWall);
		transitionList.Add (rayTransition);
		objectiveList.Add (detectorObjective);
		wallList.Add (detectorWall);
		transitionList.Add (detectorTransition);
		objectiveList.Add (cameraObjective);
		wallList.Add (cameraWall);
		transitionList.Add (cameraTransition);
		objectiveList.Add (respawnObjective);
		wallList.Add (respawnWall);
		transitionList.Add (respawnTransition);
		objectiveList.Add (colorerObjective);
		wallList.Add (colorerWall);
		transitionList.Add (colorerTransition);
		objectiveList.Add (finishObjective);
		maxActive = objectiveList.Count-1;

        Resume();
	}

    private void Update(){
        UpdateText();
		if (transObjective.IsCompleted ()) {
			transition = false;
			transObjective.SetDone (false);
			transitionList [active-1].gameObject.SetActive (false);
		}
		if (!transition)
			objectiveList [active].Check ();
		if (objectiveList [active].IsCompleted () && !transition) {
			wallList [active].gameObject.SetActive (false);
			transition = true;
			if (active < maxActive)
				active++;
		}
		tutoText.text = transition ? transObjective.GetDescription () : objectiveList [active].GetDescription ();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitTuto();
        }
	}

    private void UpdateText()
    {
        quitText.GetComponent<Text>().text = lang.GetString("tutoQuit");
        quitConfirm.GetComponentInChildren<Text>().text = lang.GetString("yes");
        quitCancel.GetComponentInChildren<Text>().text = lang.GetString("no");
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