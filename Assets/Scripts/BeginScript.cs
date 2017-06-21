using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginScript : MonoBehaviour {

	public GameObject beginCanvas;
	public GameObject timerCanvas;
	public Text levelText;
	public Camera mainCamera;
	public Camera beginCamera;

	public GameObject player;

	private string textType;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0f;
		mainCamera.enabled = false;
		beginCamera.enabled = true;
		timerCanvas.SetActive (false);
		string levelName = SceneManager.GetActiveScene ().name;
		string levelType = levelName.Substring (levelName.Length-1, 1);
		string textLevel = levelName.Substring (levelName.Length-2, 1);
		if (string.Compare(levelType,"E")==0) {
			textType="Easy";
		}
		if (string.Compare(levelType,"M")==0) {
			textType="Medium";
		}
		if (string.Compare(levelType,"H")==0) {
			textType="Hard";
		}
		levelText.text = textType + "-Level " + textLevel;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
			BeginGame ();
	}

	void BeginGame () {
		beginCanvas.SetActive (false);
		timerCanvas.SetActive (true);
		mainCamera.enabled = true;
		Time.timeScale = 1f;
		player.GetComponent<PlayerController> ().respawn = true;
		player.GetComponent<PlayerController> ().dead = true;
	}
}
