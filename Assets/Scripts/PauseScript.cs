using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PauseScript : MonoBehaviour {

	static bool pause;
	static bool pauseCheck;

	PlayerData data;
	bool volume;

	public GameObject menu;
	public Button volumeButton;
	public Text infoText;

	private string textType;

	static GameObject pauseMenu;

	void Start(){
		pause = false;
		pauseCheck = false;
		pauseMenu = menu;
		data = SaveLoad.Load ();
		if (data != null) {
			volume = !data.getVolume ();
		} else {
			volume = false;
		}
		VolumeButton ();
		Menu (false);
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
		infoText.text = textType + "-Level " + textLevel;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pause = !pause;
			pauseCheck = true;
		}
		if (pauseCheck) {
			pauseCheck = false;
			if (pause) {
				Pause ();
				Menu (true);
			} else {
				Resume ();
				Menu (false);
			}
		}
	}

	public void VolumeButton(){
		Image volumeOn = volumeButton.transform.Find ("VolumeOn").GetComponent<Image> ();
		Image volumeOff = volumeButton.transform.Find ("VolumeOff").GetComponent<Image> ();
		volume = !volume;
		volumeOn.gameObject.SetActive (volume);
		volumeOff.gameObject.SetActive (!volume);
		SaveLoad.SaveVolume (volume);
	}

	void Pause(){
		Time.timeScale = 0f;
	}

	void Resume(){
		Time.timeScale = 1f;
	}

	public void ResumeButton(){
		pause = false;
		pauseCheck = true;
	}

	public void RefreshButton(){
		/*GameObject.Find ("Player").GetComponent<PlayerController> ().Refresh ();
		GameObject.Find ("TimerCanvas").GetComponent<TimerScript> ().Reset ();
		ResumeButton ();*/
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void MenuButton(){
		SceneManager.LoadScene ("StartMenu", LoadSceneMode.Single);
	}

	public void QuitButton(){
		Application.Quit ();
	}

	void Menu(bool open){
		Cursor.visible = open;
		pauseMenu.SetActive (open);
	}
}