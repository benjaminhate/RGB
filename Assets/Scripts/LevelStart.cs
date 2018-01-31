using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour {

	public float startX;
	public float startY;
	public float startRot;

	public GameObject player;
	public Camera mainCamera;

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
        player.GetComponent<ColorElement>().ChangeColor(GetComponent<ColorElement>().GetColor());
	}
}