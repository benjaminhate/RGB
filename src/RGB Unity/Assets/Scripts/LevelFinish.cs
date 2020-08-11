using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour {
	public string nextLevel;
	private LevelManager _levelManager;

	private void Start()
	{
		_levelManager = LevelManager.Instance;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			var playerController = other.GetComponent<PlayerController>();
			if (playerController.finish) return;
			
			var timerCanvas = GameObject.Find("TimerCanvas");
			if (timerCanvas != null) {
				Debug.Log("Saving Timer");
				var levelName = _levelManager.LevelName;
					SaveLoad.SaveTimer (timerCanvas.GetComponent<TimerScript> ().GetTimer (), levelName);
				// TODO fix Serialization bug
			}

			playerController.Finish ();
		}
	}

	public void ChangeLevel()
	{
		Cursor.visible = true;
		
		_levelManager.ChangeLevel(nextLevel);
	}
}
