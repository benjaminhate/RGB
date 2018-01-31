using System.Collections;
using System.Collections.Generic;
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
	void Start () {
        ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<PlayerController>().IsMoving())
			timer += Time.deltaTime;
		timerText.text = FormatSeconds (timer);
	}

	string FormatSeconds(float timer){
		int t = (int)(timer * 100.0f);
		int minutes = t / (60 * 100);
		int seconds = (t % (60 * 100)) / 100;
		int hundredths = t % 100;
		return string.Format ("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
	}

	public void Reset(){
		timer = 0;
	}
}
