using UnityEngine;

public class AudioScript : MonoBehaviour {

    private AudioSource _source;
    private static GameObject _instance;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
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
            if (!_source.isPlaying)
            {
                _source.Play();
            }
        }
        else
        {
            AudioListener.volume = 0f;
            _source.Stop();
        }
    }
}
