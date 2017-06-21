using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective {
	public virtual string getDescription (){
		return "";
	}

	public virtual bool isCompleted (){
		return false;
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
}

public class TutoScript : MonoBehaviour {
	public GameObject MoveWall;
	public GameObject DiagWall;

	public Text tutoText;

	private MoveObjective moveObjective=new MoveObjective();
	private DiagObjective diagObjective=new DiagObjective();

	private List<Objective> objectiveList = new List<Objective>();
	private int active = 0;

	void Start(){
		objectiveList.Add (moveObjective);
		objectiveList.Add (diagObjective);
	}

	void checkMove(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		float floor = 0.15f;
		if (moveVertical > floor) {
			moveObjective.upPressed ();
		}
		if (moveVertical < -floor) {
			moveObjective.downPressed ();
		}
		if (moveHorizontal > floor) {
			moveObjective.rightPressed ();
		}
		if (moveHorizontal < -floor) {
			moveObjective.leftPressed ();
		}
	}

	void checkDiag(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		float floor = 0.3f;
		if (moveVertical > floor && moveHorizontal > floor) {
			diagObjective.uprightPressed ();
		}
		if (moveVertical < -floor && moveHorizontal < -floor) {
			diagObjective.downleftPressed ();
		}
	}

	void Update(){
		if (active == 0)
			checkMove ();
		if (moveObjective.isCompleted ()) {
			MoveWall.gameObject.SetActive (false);
			active = 1;
		}
		if (active == 1)
			checkDiag ();
		if (diagObjective.isCompleted ()) {
			DiagWall.gameObject.SetActive (false);
			//active = 1;
		}
		tutoText.text = objectiveList[active].getDescription();
	}
}