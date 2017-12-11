using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    private AudioSource source;
    private static GameObject instance;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this.gameObject;
        }
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
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else
        {
            AudioListener.volume = 0f;
            source.Stop();
        }
    }
}
