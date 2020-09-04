using Cinemachine;
using TMPro;
using UnityEngine;

public class BeginScript : MonoBehaviour {
    
	public GameObject timerCanvas;
	public TextMeshProUGUI levelText;
	public CinemachineVirtualCamera mainCamera;
	public CinemachineVirtualCamera beginCamera;
    
	public PlayerController player;
	public PauseScript pause;

    public string levelName;

	private string _textType;

    public JoystickController joystick;

    private void Start () {
        pause.pauseEnable = false;
		Time.timeScale = 0f;
		mainCamera.enabled = false;
		beginCamera.enabled = true;
		timerCanvas.SetActive (false);
		var levelType = levelName.Substring (levelName.Length-1, 1);
		var textLevel = levelName.Substring (levelName.Length-2, 1);
		if (string.CompareOrdinal(levelType,"E")==0) {
			_textType="Easy";
		}
		if (string.CompareOrdinal(levelType,"M")==0) {
			_textType="Medium";
		}
		if (string.CompareOrdinal(levelType,"H")==0) {
			_textType="Hard";
		}
		levelText.text = _textType + "-Level " + textLevel;
	}

    private void Update () {
        if (Input.anyKeyDown)
            BeginGame();
	}

    private void BeginGame () {
		gameObject.SetActive (false);
		timerCanvas.SetActive (true);
		beginCamera.enabled = false;
		mainCamera.enabled = true;
		Time.timeScale = 1f;
		player.respawn = true;
		player.dead = true;
		pause.pauseEnable = true;
#if UNITY_ANDROID
        joystick.Activate(true);
#endif
    }
}
