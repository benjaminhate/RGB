using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string LevelName { get; private set; }

    public static LevelManager Instance { get; private set; }

    private MapCreator creator;

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
        creator = GameObject.FindGameObjectWithTag("MapCreator")?.GetComponent<MapCreator>();
        LevelName = creator != null ? creator.GetMapData().GetLevelName() : SceneManager.GetActiveScene().name;
    }

    public void ChangeLevel(string levelName)
    {
        if (creator != null)
        {
            creator.ChangeLevel(levelName);
        }
        else
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }
}