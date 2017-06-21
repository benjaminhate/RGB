using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {

	static string path = Application.dataPath + "/save.gd";

	private static Scene SearchLevelNameInSave(List<Scene> scenes,string levelName){
		foreach (Scene scene in scenes) {
			if (scene.getName().CompareTo (levelName) == 0)
				return scene;
		}
		return null;
	}

	private static List<Scene> GetListOfScenes (){
		PlayerData data = Load ();
		if (data != null)
			return data.scenes;
		else
			return new List<Scene> ();
	}

	public static void SaveLevel() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		PlayerData data = new PlayerData();
		List<Scene> scenes = GetListOfScenes ();
		string levelName = SceneManager.GetActiveScene ().name;
		if (SearchLevelNameInSave (scenes, levelName)==null) {
			Scene scene = new Scene (levelName, 0);
			scenes.Add (scene);
		}
		if (!File.Exists (path)) {
			file = File.Create (path);
		}else {
			file = File.Open (path, FileMode.Open);
		}
		data.setScenes(scenes);
		data.setPath(path);
		bf.Serialize(file, data);
		file.Close();
	}

	public static void SaveTimer(float timer){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;

		string levelName = SceneManager.GetActiveScene ().name;
		List<Scene> scenes = GetListOfScenes ();
		Scene scene = SearchLevelNameInSave (scenes, levelName);
		if (scene!=null) {
			Debug.Log ("new timer " + timer + " old timer " + scene.getTimer ());
			if (timer < scene.getTimer () || scene.getTimer()==0)
				scene.setTimer (timer);
		}

		if (!File.Exists (path)) {
			file = File.Create (path);
		} else {
			file = File.Open (path,FileMode.Open);
		}

		PlayerData data = new PlayerData ();
		data.setScenes (scenes);
		data.setPath (path);
		bf.Serialize (file, data);
		file.Close ();
	}

	public static PlayerData Load() {
		if (File.Exists (path)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path, FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();
			return data;
		} else {
			return null;
		}
	}
}