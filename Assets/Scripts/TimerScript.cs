using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public Text timerText;
	public GameObject player;
	public float timer;

	public float GetTimer(){
		return timer;
	}

    public void ResetTimer()
    {
        timer = 0;
    }

	// Use this for initialization
	private void Start () {
        ResetTimer();
	}
	
	// Update is called once per frame
	private void Update () {
		if(player.GetComponent<PlayerController>().IsMoving())
			timer += Time.deltaTime;
		timerText.text = FormatSeconds (timer);
	}

	private string FormatSeconds(float timer){
		var t = (int)(timer * 100.0f);
		var minutes = t / (60 * 100);
		var seconds = (t % (60 * 100)) / 100;
		var hundredths = t % 100;
		return $"{minutes:00}:{seconds:00}.{hundredths:00}";
	}

	public void Reset(){
		timer = 0;
	}
}
