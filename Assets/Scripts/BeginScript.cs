using UnityEngine;
using UnityEngine.UI;

public class BeginScript : MonoBehaviour {
    
	public GameObject timerCanvas;
	public Text levelText;
	public Camera mainCamera;
	public Camera beginCamera;
    
	public GameObject player;

    public string levelName;

	private string textType;

    private JoystickController joystick;

    private void Start () {
#if UNITY_ANDROID
        joystick = FindObjectOfType<JoystickController>();
		joystick.Activate(false);
#endif
        GameObject.FindGameObjectWithTag("Game").GetComponent<PauseScript>().pauseEnable = false;
		Time.timeScale = 0f;
		mainCamera.enabled = false;
		beginCamera.enabled = true;
		timerCanvas.SetActive (false);
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
		levelText.text = textType + "-Level " + textLevel;
	}

    private void Update () {
        if (Input.anyKeyDown)
            BeginGame();
	}

    private void BeginGame () {
		gameObject.SetActive (false);
		timerCanvas.SetActive (true);
		mainCamera.enabled = true;
		Time.timeScale = 1f;
		player.GetComponent<PlayerController> ().respawn = true;
		player.GetComponent<PlayerController> ().dead = true;
        GameObject.FindGameObjectWithTag("Game").GetComponent<PauseScript>().pauseEnable = true;
#if UNITY_ANDROID
        joystick.Activate(true);
#endif
    }
}
