using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad {

	static string path = Application.dataPath + "/save.gd";

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

	private static List<Category> GetListOfCategories (){
		PlayerData data = Load ();
		if (data != null && data.categories != null)
			return data.categories;
		else
			return new List<Category> ();
	}

	private static bool GetVolume (){
		PlayerData data = Load ();
		if (data != null) {
			return data.volume;
		} else {
			return true;
		}
	}

	private static void setAllCompletedCategories(List<Category> categories) {
		foreach (Category category in categories) {
			if (category.getCompletedLevels ().Count == category.getLevels ().Count) {
				category.setCompleted (true);
			}
		}
	}

	public static PlayerData SaveInit(List<Category> categories){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;

		PlayerData data = new PlayerData();
		data.setCategories (categories);
		data.setVolume (GetVolume ());
		data.setPath(path);

		if (!File.Exists (path)) {
			file = File.Create (path);
		}else {
			file = File.Open (path, FileMode.Open);
		}
			
		bf.Serialize(file, data);
		file.Close();

		return data;
	}

    public static PlayerData Save(PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (!File.Exists(path))
        {
            file = File.Create(path);
        }
        else
        {
            file = File.Open(path, FileMode.Open);
        }

        bf.Serialize(file, data);
        file.Close();

        return data;
    }

	public static PlayerData SaveLevel() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;

		List<Category> categories = GetListOfCategories ();
		string levelName = SceneManager.GetActiveScene ().name;
		Level level = SearchLevelNameInSave (categories, levelName);
		if (level != null) {
			level.setBlocked (false);
		}

		Category category = SearchLevelNameInCategory (categories, levelName);
		if (category != null) {
			category.setBlocked (false);
		}
		setAllCompletedCategories (categories);

		PlayerData data = new PlayerData();
		data.setCategories (categories);
		data.setVolume (GetVolume ());
		data.setPath(path);

		if (!File.Exists (path)) {
			file = File.Create (path);
		}else {
			file = File.Open (path, FileMode.Open);
		}

		bf.Serialize(file, data);
		file.Close();

		return data;
	}

	public static PlayerData SaveTimer(float timer){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;

		string levelName = SceneManager.GetActiveScene ().name;
		List<Category> categories = GetListOfCategories ();
		Level level = SearchLevelNameInSave (categories, levelName);
		if (level != null) {
			level.setCompleted (true);
			level.setBlocked (false);
			if (timer < level.getTimer () || level.getTimer () == 0)
				level.setTimer (timer);
		}

		setAllCompletedCategories (categories);

		PlayerData data = new PlayerData ();
		data.setVolume (GetVolume ());
		data.setCategories (categories);
		data.setPath (path);

		if (!File.Exists (path)) {
			file = File.Create (path);
		} else {
			file = File.Open (path,FileMode.Open);
		}

		bf.Serialize (file, data);
		file.Close ();

		return data;
	}

	public static PlayerData SaveVolume(bool volume){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;

		PlayerData data = new PlayerData ();
		data.setCategories (GetListOfCategories ());
		data.setPath (path);
		data.setVolume (volume);

		if (!File.Exists (path)) {
			file = File.Create (path);
		} else {
			file = File.Open (path,FileMode.Open);
		}

		bf.Serialize (file, data);
		file.Close ();

		return data;
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