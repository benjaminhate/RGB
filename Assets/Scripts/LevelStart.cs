using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour {

	public float startX;
	public float startY;
	public float startRot;

	public GameObject player;
	public Camera mainCamera;

	public enum Colors {RED,GREEN,BLUE};
	public Colors levelColor;

	private Color red = new Color (1f, 0.2f, 0.2f, 1f);
	private Color green = new Color(0f,0.8f,0f,1f);
	private Color blue = new Color (0.1f, 0.3f, 1f, 1f);

	void Start(){
		Cursor.visible = false;
		SaveLoad.SaveLevel ();
		PlacePlayer ();
	}

	void Update(){
		if (player.GetComponent<PlayerController> ().respawn) {
			StartCoroutine (Respawn ());
		}
	}

	private IEnumerator WaitForAnimation ( Animation animation )
	{
		do
		{
			yield return null;
		} while ( animation.isPlaying );
	}

	IEnumerator Respawn(){
		PlacePlayer ();
		player.GetComponent<PlayerController> ().respawn = false;
		Animation anim = player.GetComponent<Animation> ();
		anim.PlayQueued ("RespawnAnimation");
		yield return StartCoroutine (WaitForAnimation (anim));
		player.GetComponent<PlayerController> ().dead = false;
	}

	void PlacePlayer(){
		player.transform.position = new Vector3 (startX, startY, 0);
		player.transform.eulerAngles = new Vector3 (0, 0, startRot);
		mainCamera.transform.position = new Vector3 (startX, startY, -1);
		SpriteRenderer renderer = player.GetComponent<SpriteRenderer> ();
		switch (levelColor){
		case Colors.RED:
			renderer.color = red;
			break;
		case Colors.GREEN:
			renderer.color = green;
			break;
		case Colors.BLUE:
			renderer.color = blue;
			break;
		default:
			renderer.color = red;
			break;
		}
	}
}