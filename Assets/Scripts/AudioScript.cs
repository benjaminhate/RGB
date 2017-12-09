using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

	void Start () {
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        bool volume = SaveLoad.Load().getVolume();
        if (volume)
        {
            AudioListener.volume = 1f;
            source.Play();
        }
        else
        {
            AudioListener.volume = 0f;
            source.Stop();
        }
    }
}
