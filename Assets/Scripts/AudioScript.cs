using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    private AudioSource source;
    private static GameObject _instance;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = gameObject;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start () {
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        var data = SaveLoad.Load();
        var volume = false;
        if (data != null)
        {
            volume = data.GetVolume();
        }
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
