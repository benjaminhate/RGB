using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objective {
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
		return "Allez jusqu'à la prochaine salle pour continuer";
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
		string upColor = up ? "green" : "red";
		string downColor = down ? "green" : "red";
		string rightColor = right ? "green" : "red";
		string leftColor = left ? "green" : "red";
		return "Appuyez sur Z,Q,S,D ou sur les flèches directionnelles pour vous déplacer.\n" +
			"<color="+upColor+">Haut</color>\n" +
			"<color="+downColor+">Bas</color>\n" +
			"<color="+rightColor+">Droite</color>\n" +
			"<color="+leftColor+">Gauche</color>";
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
		string uprightColor = upright ? "green" : "red";
		string downleftColor = downleft ? "green" : "red";
		return "Appuyez sur une touche haut/bas et une touche gauche/droite en même temps pour vous déplacez en diagonale.\n" +
		"<color=" + uprightColor + ">Diagonale haut droite</color>\n" +
		"<color=" + downleftColor + ">Diagonale bas gauche</color>\n";
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
		string deadColor = rayDead ? "green" : "red";
		return "Le rayon est un obstacle.\n" +
			"Comme tous les obstacles, si vous le touchez sans avoir la même couleur, vous mourez.\n" +
			"Le rayon peut se déplacer d'avant en arrière ou ne pas bouger.\n" +
			"<color="+deadColor+">Mourez sur le rayon </color>";
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
		string deadColor = detectorDead ? "green" : "red";
		return "Le détecteur est un obstacle.\n" +
			"Il peut s'étendre et se détendre ou ne pas bouger.\n" +
			"<color="+deadColor+">Mourez sur le détecteur</color>";
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
		string deadColor = cameraDead ? "green" : "red";
		return "La caméra est un obstacle.\n" +
			"Elle ne peut que tourner.\n" +
			"<color="+deadColor+">Mourez sur la caméra</color>";
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
		string keyColor = key ? "green" : "red";
		return "Si vous appuyez sur la touche R, vous recommencez le niveau.\n" +
			"<color="+keyColor+">Appuyez sur R</color>";
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
		string redColor = red ? "green" : "red";
		string greenColor = green ? "green" : "red";
		string blueColor = blue ? "green" : "red";
		return "Le coloreur n'est PAS un obstacle.\n" +
			"Il permet de changer de couleur.\n" +
			"<color="+redColor+">Passez en couleur rouge</color>\n" +
			"<color="+blueColor+">Passez en couleur bleue</color>\n" +
			"<color="+greenColor+">Passez en couleur verte</color>\n";
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
		return "Allez sur la fin afin de finir le tutoriel.";
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