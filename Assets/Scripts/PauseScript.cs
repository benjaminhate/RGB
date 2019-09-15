using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Objects;

public class PauseScript : MonoBehaviour {
	private static bool _pause;
	private static bool _pauseCheck;

	private PlayerData data;
	private bool volume;

	public GameObject menu;
	public Button volumeButton;
	public Text infoText;
    public bool pauseEnable;

	private string textType;

    public string levelName;

    private static GameObject _pauseMenu;

    private JoystickController joystick;

    private void Start(){
#if UNITY_ANDROID
        joystick = GameObject.FindObjectOfType<JoystickController>();
#endif
		_pause = false;
		_pauseCheck = false;
		_pauseMenu = menu;
		data = SaveLoad.Load ();
		if (data != null) {
			volume = !data.GetVolume ();
		} else {
			volume = false;
		}
		VolumeButton ();
		Menu (false);
		var levelType = levelName.Substring (levelName.Length-1, 1);
		var textLevel = levelName.Substring (levelName.Length-2, 1);
		if (string.CompareOrdinal(levelType,"E")==0) {
			textType="Easy";
		}
		if (string.CompareOrdinal(levelType,"M")==0) {
			textType="Medium";
		}
		if (string.CompareOrdinal(levelType,"H")==0) {
			textType="Hard";
		}
		infoText.text = textType + "-Level " + textLevel;
	}

    private void Update(){
		if (Input.GetKeyDown (KeyCode.Escape) && pauseEnable) {
			_pause = !_pause;
			_pauseCheck = true;
		}
		if (_pauseCheck) {
			_pauseCheck = false;
			if (_pause) {
				Pause ();
				Menu (true);
			} else {
				Resume ();
				Menu (false);
			}
		}
	}

	public void VolumeButton(){
		var volumeOn = volumeButton.transform.Find ("VolumeOn").GetComponent<Image> ();
		var volumeOff = volumeButton.transform.Find ("VolumeOff").GetComponent<Image> ();
		volume = !volume;
		volumeOn.gameObject.SetActive (volume);
		volumeOff.gameObject.SetActive (!volume);
		SaveLoad.SaveVolume (volume);
        var audio = GameObject.FindGameObjectWithTag("Audio");
        if (audio != null)
        {
            audio.GetComponent<AudioScript>().UpdateVolume();
        }
	}

	private void Pause(){
		Time.timeScale = 0f;
	}

	private void Resume(){
		Time.timeScale = 1f;
	}

	public void ResumeButton(){
		_pause = false;
		_pauseCheck = true;
	}

	public void RefreshButton(){
        var creator = GameObject.FindGameObjectWithTag("MapCreator").GetComponent<MapCreator>();
        var levelName = creator.GetMapData().GetLevelName();
        creator.ChangeLevel(levelName);
	}

	public void MenuButton(){
		SceneManager.LoadScene ("StartMenu", LoadSceneMode.Single);
	}

	public void QuitButton(){
		Application.Quit ();
	}

	private void Menu(bool open){
#if UNITY_ANDROID
        joystick.Activate(!open);
#endif
		Cursor.visible = open;
		_pauseMenu.SetActive (open);
	}
}