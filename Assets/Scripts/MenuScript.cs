using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuScript : MonoBehaviour {

	public Text text;
	public Button resetButton;
	PlayerData data = new PlayerData ();

	void Start(){
		MenuStart ();
	}

	void MenuStart(){
		data = SaveLoad.Load ();
		if (data == null) {
			text.text = "Start";
			resetButton.gameObject.SetActive (false);
		} else {
			text.text = "Continue";
			resetButton.gameObject.SetActive (true);
		}
	}

	public void StartGame(){
		SceneManager.LoadScene ("LevelMenu", LoadSceneMode.Single);
	}

	public void TutoGame(){
		SceneManager.LoadScene ("LevelTuto", LoadSceneMode.Single);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void ResetGame(){
		if (File.Exists (data.path)) {
			File.Delete (data.path);
			MenuStart ();
		}
	}
}
