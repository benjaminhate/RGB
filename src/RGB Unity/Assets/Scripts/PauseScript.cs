using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Objects;
using TMPro;

public class PauseScript : MonoBehaviour {
	private static bool _pause;
	private static bool _pauseCheck;

	private PlayerData _data;
	private bool _volume;

	public GameObject menu;
	public Button volumeButton;
	public TextMeshProUGUI infoText;
    public bool pauseEnable;

	private string _textType;

    private string _levelName;

    private static GameObject _pauseMenu;

    public JoystickController joystick;

    private LevelManager _levelManager;

    private void Start(){
		_pause = false;
		_pauseCheck = false;
		_pauseMenu = menu;
		_data = SaveLoad.Load ();
		if (_data != null) {
			_volume = !_data.GetVolume ();
		} else {
			_volume = false;
		}
		VolumeButton ();
		Menu (false);

		_levelManager = LevelManager.Instance;
		
		_levelName = _levelManager.LevelName;
		
		var levelType = _levelName.Substring (_levelName.Length-1, 1);
		var textLevel = _levelName.Substring (_levelName.Length-2, 1);
		if (string.CompareOrdinal(levelType,"E")==0) {
			_textType="Easy";
		}
		if (string.CompareOrdinal(levelType,"M")==0) {
			_textType="Medium";
		}
		if (string.CompareOrdinal(levelType,"H")==0) {
			_textType="Hard";
		}
		infoText.text = _textType + "-Level " + textLevel;
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
		_volume = !_volume;
		volumeOn.gameObject.SetActive (_volume);
		volumeOff.gameObject.SetActive (!_volume);
		SaveLoad.SaveVolume (_volume);
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

	public void RefreshButton()
	{
		_levelManager.ChangeLevel(_levelName);
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