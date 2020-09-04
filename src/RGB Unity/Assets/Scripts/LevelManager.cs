using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string LevelName { get; private set; }

    public static LevelManager Instance { get; private set; }

    private MapCreator _creator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _creator = GameObject.FindGameObjectWithTag("MapCreator")?.GetComponent<MapCreator>();
        LevelName = _creator != null ? _creator.GetMapData().GetLevelName() : SceneManager.GetActiveScene().name;
    }

    public void ChangeLevel(string levelName)
    {
        if (_creator != null)
        {
            _creator.ChangeLevel(levelName);
        }
        else
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }
}