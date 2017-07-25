using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuScript : MonoBehaviour {

	public Button volumeButton;
	public Button resetButton;
	PlayerData data = new PlayerData ();
	bool volume;

	public List<Category> categories;

	private bool settings;
	private Image confirm;


	void Start(){
		settings = true;
		confirm = resetButton.transform.Find ("Confirmation").GetComponent<Image> ();
		MenuStart ();
	}

	void MenuStart(){
		SettingsGame ();
		data = SaveLoad.Load ();
		if (data == null) {
			data = SaveLoad.SaveInit (categories);
		}
		volume = !data.getVolume ();
		VolumeGame ();
		confirm.gameObject.SetActive (false);
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

	public void SettingsGame(){
		settings = !settings;
		volumeButton.gameObject.SetActive (settings);
		resetButton.gameObject.SetActive (settings);
	}

	public void VolumeGame(){
		Image volumeOn = volumeButton.transform.Find ("Volume").GetComponent<Image> ();
		Image volumeOff = volumeButton.transform.Find ("Volume off").GetComponent<Image> ();
		volume = !volume;
		volumeOn.gameObject.SetActive (volume);
		volumeOff.gameObject.SetActive (!volume);
		SaveLoad.SaveVolume (volume);
	}

	public void ResetGame(){
		confirm.gameObject.SetActive (true);
	}

	public void ConfirmationResetGame(){
		SaveLoad.SaveInit (categories);
		settings = false;
		MenuStart ();
		confirm.gameObject.SetActive (false);
	}

	public void CancelResetGame(){
		confirm.gameObject.SetActive (false);
	}
}
