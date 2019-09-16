using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour {
	public string nextLevel;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			var playerController = other.GetComponent<PlayerController>();
			if (playerController.finish) return;
			
			var timerCanvas = GameObject.Find("TimerCanvas");
			if (timerCanvas != null) {
				Debug.Log("Saving Timer");
				var mapCreator = GameObject.FindGameObjectWithTag("MapCreator")?.GetComponent<MapCreator>();
				var levelName = mapCreator == null ? SceneManager.GetActiveScene().name : mapCreator.GetMapData().GetLevelName();
//					SaveLoad.SaveTimer (timerCanvas.GetComponent<TimerScript> ().GetTimer (), levelName);
				// TODO fix Serialization bug
			}

			StartCoroutine (playerController.Finish ());
		}
	}

	public void ChangeLevel()
	{
		Cursor.visible = true;
		
		var mapCreator = GameObject.FindGameObjectWithTag("MapCreator")?.GetComponent<MapCreator>();
		if (mapCreator == null)
		{
			SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
		}
		else
		{
			mapCreator.ChangeLevel(nextLevel);
		}
	}
}
