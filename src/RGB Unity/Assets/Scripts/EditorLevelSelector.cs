using UnityEngine;
using System.IO;

public class EditorLevelSelector : MonoBehaviour {



	// Use this for initialization
    private void Start () {
        GetAllDirectories(Path.Combine(Application.persistentDataPath, "maps"));
        GetAllFiles(Path.Combine(Application.persistentDataPath,"maps"));
    }

    private void GetAllFiles(string path)
    {
        var info = new DirectoryInfo(path);
        foreach(var file in info.GetFiles())
        {
            Debug.Log("file : " + file.Name);
        }
    }

    private void GetAllDirectories(string path)
    {
        var info = new DirectoryInfo(path);
        foreach (var dir in info.GetDirectories())
        {
            Debug.Log("directory : " + dir.Name);
        }
    }
}
