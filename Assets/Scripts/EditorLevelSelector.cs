using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EditorLevelSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetAllDirectories(Path.Combine(Application.persistentDataPath, "maps"));
        GetAllFiles(Path.Combine(Application.persistentDataPath,"maps"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GetAllFiles(string path)
    {
        DirectoryInfo info = new DirectoryInfo(path);
        foreach(FileInfo file in info.GetFiles())
        {
            Debug.Log("file : " + file.Name);
        }
    }

    private void GetAllDirectories(string path)
    {
        DirectoryInfo info = new DirectoryInfo(path);
        foreach (DirectoryInfo dir in info.GetDirectories())
        {
            Debug.Log("directory : " + dir.Name);
        }
    }
}
