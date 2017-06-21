using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuScript : MonoBehaviour {

	public string Level;

	public void Replay(){
		SceneManager.LoadScene (Level, LoadSceneMode.Single);
	}

	public void Quit(){
		Application.Quit ();
	}
}
