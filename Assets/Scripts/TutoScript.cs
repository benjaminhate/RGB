using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objective {

    protected LanguageController lang;

    public void Init()
    {
        lang = new LanguageController(SaveLoad.Load().getLanguage());
    }

    public virtual string getDescription (){
        return "";
	}

	public virtual bool isCompleted (){
		return false;
	}

	public virtual void check (){
	}
}

public class TransitionObjective : Objective {
	private bool done;

	public void setDone(bool doneChange){
		done = doneChange;
	}

	public override string getDescription(){
        base.Init();
        return lang.GetString("tutoTransition");
	}

	public override bool isCompleted(){
		return done;
	}
}

public class MoveObjective : Objective {

	private bool up;
	private bool down;
	private bool left;
	private bool right;

	public void upPressed(){
		up = true;
	}
	public void downPressed(){
		down = true;
	}
	public void leftPressed(){
		left = true;
	}
	public void rightPressed(){
		right = true;
	}

	public override string getDescription(){
        base.Init();
        string upColor = up ? "green" : "red";
		string downColor = down ? "green" : "red";
		string rightColor = right ? "green" : "red";
		string leftColor = left ? "green" : "red";
        return lang.GetString("tutoMove") + "\n" +
            "<color=" + upColor + ">" + lang.GetString("tutoMoveUp") + "</color>\n" +
            "<color=" + downColor + ">" + lang.GetString("tutoMoveDown") + "</color>\n" +
            "<color=" + rightColor + ">" + lang.GetString("tutoMoveRight") + "</color>\n" +
            "<color=" + leftColor + ">" + lang.GetString("tutoMoveLeft") + "</color>";
	}

	public override bool isCompleted(){
		return (up && down && left && right);
	}

	public override void check(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		float floor = 0.15f;
		if (moveVertical > floor && !GameObject.Find("Player").GetComponent<PlayerController> ().dead) {
			upPressed ();
		}
		if (moveVertical < -floor && !GameObject.Find("Player").GetComponent<PlayerController> ().dead) {
			downPressed ();
		}
		if (moveHorizontal > floor && !GameObject.Find("Player").GetComponent<PlayerController> ().dead) {
			rightPressed ();
		}
		if (moveHorizontal < -floor && !GameObject.Find("Player").GetComponent<PlayerController> ().dead) {
			leftPressed ();
		}
	}
}

public class DiagObjective : Objective {

	private bool upright;
	private bool downleft;

	public void uprightPressed(){
		upright = true;
	}
	public void downleftPressed(){
		downleft = true;
	}

	public override string getDescription(){
        base.Init();
        string uprightColor = upright ? "green" : "red";
		string downleftColor = downleft ? "green" : "red";
        return lang.GetString("tutoDiagonal") + "\n" +
        "<color=" + uprightColor + ">" + lang.GetString("tutoDiagonalUpRight") + "</color>\n" +
        "<color=" + downleftColor + ">" + lang.GetString("tutoDiagonalDownLeft") + "</color>\n";
	}

	public override bool isCompleted(){
		return (upright && downleft);
	}

	public override void check(){
		LevelStart start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=7.25f;
		start.startY=9.5f;
		start.startRot = 180;
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		float floor = 0.3f;
		if (moveVertical > floor && moveHorizontal > floor && !GameObject.Find("Player").GetComponent<PlayerController> ().dead) {
			uprightPressed ();
		}
		if (moveVertical < -floor && moveHorizontal < -floor && !GameObject.Find("Player").GetComponent<PlayerController> ().dead) {
			downleftPressed ();
		}
	}
}

public class RayObjective : Objective {
	private bool rayDead;

	public void Raydead(){
		rayDead = true;
	}

	public override string getDescription(){
        base.Init();
        string deadColor = rayDead ? "green" : "red";
        return lang.GetString("tutoRay") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoRayDeath") + "</color>";
	}

	public override bool isCompleted(){
		return rayDead;
	}

	public override void check(){
		LevelStart start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=7.25f;
		start.startY=-1.5f;
		start.startRot = 180;
		if (GameObject.Find("Player").GetComponent<PlayerController> ().obstacleKill && GameObject.Find ("Player").GetComponent<PlayerController> ().obstacle.Contains("Ray"))
			Raydead ();
	}
}

public class DetectorObjective : Objective {
	private bool detectorDead;

	public void Detectordead(){
		detectorDead = true;
	}

	public override string getDescription(){
        base.Init();
        string deadColor = detectorDead ? "green" : "red";
        return lang.GetString("tutoDetector") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoDetectorDeath") + "</color>";
	}

	public override bool isCompleted(){
		return detectorDead;
	}

	public override void check (){
		LevelStart start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=7.25f;
		start.startY=-11.5f;
		start.startRot = 180;
		if (GameObject.Find("Player").GetComponent<PlayerController> ().obstacleKill && GameObject.Find ("Player").GetComponent<PlayerController> ().obstacle.Contains("Detector"))
			Detectordead ();
	}
}

public class CameraObjective : Objective {
	private bool cameraDead;

	public void Cameradead(){
		cameraDead = true;
	}

	public override string getDescription(){
        base.Init();
        string deadColor = cameraDead ? "green" : "red";
        return lang.GetString("tutoCamera") + "\n" +
            "<color=" + deadColor + ">" + lang.GetString("tutoCameraDeath") + "</color>";
	}

	public override bool isCompleted(){
		return cameraDead;
	}

	public override void check (){
		LevelStart start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-2f;
		start.startY=-16f;
		start.startRot = 90;
		if (GameObject.Find("Player").GetComponent<PlayerController> ().obstacleKill && GameObject.Find ("Player").GetComponent<PlayerController> ().obstacle.Contains("Camera"))
			Cameradead ();
	}
}

public class RespawnObjective : Objective {
	private bool key;

	public void keyPressed(){
		key = true;
	}

	public override string getDescription (){
        base.Init();
        string keyColor = key ? "green" : "red";
        return lang.GetString("tutoRespawn") + "\n" +
            "<color=" + keyColor + ">" + lang.GetString("tutoRespawnTouch") + "</color>";
	}

	public override bool isCompleted (){
		return key;
	}

	public override void check (){
		LevelStart start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-7.25f;
		start.startY=-8.5f;
		start.startRot = 0;
		if (Input.GetKeyDown (KeyCode.R))
			keyPressed ();
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

	public void redChange(){
		red = true;
	}

	public void blueChange(){
		blue = true;
	}

	public void greenChange(){
		green = true;
	}

	bool CompareColor(Color color1,Color color2,bool init){
		if (color1.r == color2.r && color1.g == color2.g && color1.b == color2.b && color1.a == color2.a && init)
			return true;
		else
			return false;
	}

	public override string getDescription (){
        base.Init();
        string redColor = red ? "green" : "red";
		string greenColor = green ? "green" : "red";
		string blueColor = blue ? "green" : "red";
        return lang.GetString("tutoColorer") + "\n" +
            "<color=" + greenColor + ">" + lang.GetString("tutoColorerGreen") + "</color>\n" +
            "<color=" + blueColor + ">" + lang.GetString("tutoColorerBlue") + "</color>\n" +
            "<color=" + redColor + ">" + lang.GetString("tutoColorerRed") + "</color>\n";
	}

	public override bool isCompleted (){
		return (red && blue && green);
	}

	public override void check (){
		LevelStart start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-7.25f;
		start.startY=1.5f;
		start.startRot = 0;
		if (CompareColor (GameObject.Find ("Player").GetComponent<SpriteRenderer> ().color, redC,init))
			red = true;
		if (CompareColor (GameObject.Find ("Player").GetComponent<SpriteRenderer> ().color, greenC, true)) {
			green = true;
			init = true;
		}
		if (CompareColor (GameObject.Find ("Player").GetComponent<SpriteRenderer> ().color, blueC, true)) {
			blue = true;
			init = true;
		}
	}
}

public class FinishObjective : Objective {
	public override void check (){
		LevelStart start = GameObject.Find ("LevelStart").GetComponent<LevelStart> ();
		start.startX=-7.25f;
		start.startY=11.5f;
		start.startRot = 0;
	}
	public override string getDescription (){
        base.Init();
        return lang.GetString("tutoEnd");
	}
}

public class TutoScript : MonoBehaviour {
	public GameObject MoveWall;
	public GameObject DiagWall;
	public GameObject RayWall;
	public GameObject DetectorWall;
	public GameObject CameraWall;
	public GameObject RespawnWall;
	public GameObject ColorerWall;

	public GameObject MoveTransition;
	public GameObject DiagTransition;
	public GameObject RayTransition;
	public GameObject DetectorTransition;
	public GameObject CameraTransition;
	public GameObject RespawnTransition;
	public GameObject ColorerTransition;

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
	private int active = 0;
	private int maxActive=0;

    private LanguageController lang;
    private GameObject quitText;
    private GameObject quitConfirm;
    private GameObject quitCancel;

    void Awake()
    {
        lang = new LanguageController(SaveLoad.Load().getLanguage());
        quitText = GameObject.Find("QuitText");
        quitConfirm = GameObject.Find("QuitConfirm");
        quitCancel = GameObject.Find("QuitCancel");
    }

    void Start(){
		objectiveList.Add (moveObjective);
		wallList.Add (MoveWall);
		transitionList.Add (MoveTransition);
		objectiveList.Add (diagObjective);
		wallList.Add (DiagWall);
		transitionList.Add (DiagTransition);
		objectiveList.Add (rayObjective);
		wallList.Add (RayWall);
		transitionList.Add (RayTransition);
		objectiveList.Add (detectorObjective);
		wallList.Add (DetectorWall);
		transitionList.Add (DetectorTransition);
		objectiveList.Add (cameraObjective);
		wallList.Add (CameraWall);
		transitionList.Add (CameraTransition);
		objectiveList.Add (respawnObjective);
		wallList.Add (RespawnWall);
		transitionList.Add (RespawnTransition);
		objectiveList.Add (colorerObjective);
		wallList.Add (ColorerWall);
		transitionList.Add (ColorerTransition);
		objectiveList.Add (finishObjective);
		maxActive = objectiveList.Count-1;

        Resume();
	}

	void Update(){
        UpdateText();
		if (transObjective.isCompleted ()) {
			transition = false;
			transObjective.setDone (false);
			transitionList [active-1].gameObject.SetActive (false);
		}
		if (!transition)
			objectiveList [active].check ();
		if (objectiveList [active].isCompleted () && !transition) {
			wallList [active].gameObject.SetActive (false);
			transition = true;
			if (active < maxActive)
				active++;
		}
		if (transition) {
			tutoText.text = transObjective.getDescription ();
		} else {
			tutoText.text = objectiveList [active].getDescription ();
		}

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

    void QuitTuto()
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
}