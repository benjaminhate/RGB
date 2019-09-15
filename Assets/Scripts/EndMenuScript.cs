using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuScript : MonoBehaviour {

	public string level;

	public void Replay(){
		SceneManager.LoadScene (level, LoadSceneMode.Single);
	}

	public void Quit(){
		Application.Quit ();
	}
}
