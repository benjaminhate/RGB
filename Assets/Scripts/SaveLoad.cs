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

	private static Level SearchLevelNameInSave(List<Category> categories,string levelName){
		foreach (Category category in categories) {
			foreach (Level level in category.getLevels()) {
				if (level.getSceneName ().CompareTo (levelName) == 0)
					return level;
			}
		}
		return null;
	}

	private static Category SearchLevelNameInCategory(List<Category> categories,string levelName){
		foreach (Category category in categories) {
			foreach (Level level in category.getLevels()) {
				if (level.getSceneName ().CompareTo (levelName) == 0)
					return category;
			}
		}
		return null;
	}

	private static List<Scene> GetListOfScenes (){
		PlayerData data = Load ();
		if (data != null && data.scenes != null)
			return data.scenes;
		else
			return new List<Scene> ();
	}

	private static List<Category> GetListOfCategories (){
		PlayerData data = Load ();
		if (data != null && data.categories != null)
			return data.categories;
		else
			return new List<Category> ();
	}

	private static void setAllCompletedCategories(List<Category> categories) {
		foreach (Category category in categories) {
			if (category.getCompletedLevels ().Count == category.getLevels ().Count) {
				category.setCompleted (true);
			}
		}
	}

	public static void SaveInit(List<Category> categories){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		PlayerData data = new PlayerData();
		if (!File.Exists (path)) {
			file = File.Create (path);
		}else {
			file = File.Open (path, FileMode.Open);
		}
		data.setCategories (categories);
		data.setPath(path);
		bf.Serialize(file, data);
		file.Close();
	}

	public static void SaveLevel() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;
		PlayerData data = new PlayerData();
		List<Scene> scenes = GetListOfScenes ();
		List<Category> categories = GetListOfCategories ();
		string levelName = SceneManager.GetActiveScene ().name;
		if (SearchLevelNameInSave (scenes, levelName)==null) {
			Scene scene = new Scene (levelName, 0);
			scenes.Add (scene);
		}
		Level level = SearchLevelNameInSave (categories, levelName);
		if (level != null) {
			level.setBlocked (false);
		}

		Category category = SearchLevelNameInCategory (categories, levelName);
		if (category != null) {
			category.setBlocked (false);
		}

		setAllCompletedCategories (categories);

		if (!File.Exists (path)) {
			file = File.Create (path);
		}else {
			file = File.Open (path, FileMode.Open);
		}
		data.setCategories (categories);
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
			if (timer < scene.getTimer () || scene.getTimer () == 0)
				scene.setTimer (timer);
		}

		List<Category> categories = GetListOfCategories ();
		Level level = SearchLevelNameInSave (categories, levelName);
		if (level != null) {
			level.setCompleted (true);
			level.setBlocked (false);
			if (timer < level.getTimer () || level.getTimer () == 0)
				level.setTimer (timer);
		}

		setAllCompletedCategories (categories);

		if (!File.Exists (path)) {
			file = File.Create (path);
		} else {
			file = File.Open (path,FileMode.Open);
		}

		PlayerData data = new PlayerData ();
		data.setScenes (scenes);
		data.setCategories (categories);
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