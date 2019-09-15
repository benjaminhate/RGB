using System.Collections;
using Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour {

	public float startX;
	public float startY;
	public float startRot;

	public GameObject player;
	public Camera mainCamera;

	private void Start(){
		Cursor.visible = false;
		var mapCreator = GameObject.FindGameObjectWithTag("MapCreator");
        var levelName = mapCreator == null ? SceneManager.GetActiveScene().name : mapCreator.GetComponent<MapCreator>()?.GetMapData().GetLevelName();
        Debug.Log(levelName);
        SaveLoad.SaveLevel (levelName);
		PlacePlayer ();
	}

	private void Update(){
		if (player.GetComponent<PlayerController> ().respawn) {
			StartCoroutine (Respawn ());
		}
	}

	private IEnumerator WaitForAnimation ( Animation animationPlaying )
	{
		do
		{
			yield return null;
		} while ( animationPlaying.isPlaying );
	}

	private IEnumerator Respawn(){
		PlacePlayer ();
		player.GetComponent<PlayerController> ().respawn = false;
		var anim = player.GetComponent<Animation> ();
		anim.PlayQueued ("RespawnAnimation");
		yield return StartCoroutine (WaitForAnimation (anim));
		player.GetComponent<PlayerController> ().dead = false;
	}

	private void PlacePlayer(){
		player.transform.position = new Vector3 (startX, startY, 0);
		player.transform.eulerAngles = new Vector3 (0, 0, startRot);
		mainCamera.transform.position = new Vector3 (startX, startY, -1);
        player.GetComponent<ColorElement>().ChangeColor(GetComponent<ColorElement>().colorSo);
	}
}