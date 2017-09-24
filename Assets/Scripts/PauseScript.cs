using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

	static bool pause = false;

	static public GameObject menu;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pause = !pause;
			if (pause) {
				Pause ();
				Menu (true);
			} else {
				Resume ();
				Menu (false);
			}
		}
	}

	void Pause(){
		Time.timeScale = 0f;
	}

	void Resume(){
		Time.timeScale = 1f;
	}

	public void ResumeButton(){
		pause = false;
	}

	public void MenuButton(){
		SceneManager.LoadScene ("StartMenu", LoadSceneMode.Single);
	}

	public void QuitButton(){
		Application.Quit ();
	}

	void Menu(bool open){
		Cursor.visible = open;
		menu.SetActive (open);
	}
}