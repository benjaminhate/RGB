using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartMenu : MonoBehaviour {
    private void Awake()
    {
        if (SaveLoad.Load() == null || SaveLoad.Load().firstTime)
        {
            FirstTimeLaunch.LoadAll();
            var firstTime = new GameObject("First Time"){tag = "First Time"};
            DontDestroyOnLoad(firstTime);
            //SaveLoad.SaveFirstTime(false);
            Debug.Log("LoadedFirstTimeData");
        }
        Debug.Log("LoadedMenu");
        SceneManager.LoadScene("StartMenu");
    }
}
