using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad {

	static string path = Path.Combine(Application.persistentDataPath,"save.gd");

	private static Level SearchLevelNameInSave(List<Category> categories,string levelName){
        Debug.Log("Level name : " + levelName);
		foreach (Category category in categories) {
			foreach (Level level in category.getLevels()) {
                Debug.Log("Level in Save : " + level.getSceneName());
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

    private static bool GetTutorial()
    {
        PlayerData data = Load();
        if (data != null)
        {
            return data.tutorial;
        }
        else
        {
            return false;
        }
    }

    private static string GetLanguage()
    {
        PlayerData data = Load();
        if (data != null)
        {
            return data.language;
        }
        else
        {
            return "English";
        }
    }

    private static bool GetFirstTime()
    {
        PlayerData data = Load();
        if (data != null)
        {
            return data.firstTime;
        }
        else
        {
            return true;
        }
    }

    private static void SetAllCompletedCategories(List<Category> categories) {
		foreach (Category category in categories) {
			if (category.getCompletedLevels ().Count == category.getLevels ().Count) {
				category.setCompleted (true);
			}
		}
	}

    private static PlayerData SaveData(PlayerData data)
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

	public static PlayerData SaveInit(List<Category> categories){

        PlayerData data = LoadNew();
		data.setCategories (categories);

        return SaveData(data);
		
	}

    public static PlayerData Save(PlayerData data)
    {
        return SaveData(data);
    }

	public static PlayerData SaveLevel(string levelName) {
		List<Category> categories = GetListOfCategories ();
		Level level = SearchLevelNameInSave (categories, levelName);
		if (level != null) {
			level.setBlocked (false);
		}

		Category category = SearchLevelNameInCategory (categories, levelName);
		if (category != null) {
			category.setBlocked (false);
		}
		SetAllCompletedCategories (categories);

        PlayerData data = LoadNew();
        data.setCategories(categories);
        //new PlayerData(categories, path, GetVolume(), GetTutorial(), GetLanguage(), GetFirstTime());

        return SaveData(data);
	}

	public static PlayerData SaveTimer(float timer){
		string levelName = SceneManager.GetActiveScene ().name;
		List<Category> categories = GetListOfCategories ();
		Level level = SearchLevelNameInSave (categories, levelName);
		if (level != null) {
			level.setCompleted (true);
			level.setBlocked (false);
			if (timer < level.getTimer () || level.getTimer () == 0)
				level.setTimer (timer);
		}

		SetAllCompletedCategories (categories);

        PlayerData data = LoadNew();
        data.setCategories(categories);
        //new PlayerData (categories,path,GetVolume(),GetTutorial(),GetLanguage(),GetFirstTime());

        return SaveData(data);
    }

	public static PlayerData SaveVolume(bool volume){

        PlayerData data = LoadNew();
        data.setVolume(volume);
        //new PlayerData (GetListOfCategories(), path, volume, GetTutorial(), GetLanguage(), GetFirstTime());

        return SaveData(data);
    }

    public static PlayerData SaveTutorial(bool tutorial)
    {
        PlayerData data = LoadNew();
        data.setTutorial(tutorial);
        //new PlayerData(GetListOfCategories(), path, GetVolume(), tutorial, GetLanguage(), GetFirstTime());

        return SaveData(data);
    }

    public static PlayerData SaveLanguage(string language)
    {
        PlayerData data = LoadNew();
        data.setLanguage(language);
        //new PlayerData(GetListOfCategories(), path, GetVolume(), GetTutorial(), language, GetFirstTime());

        return SaveData(data);
    }

    public static PlayerData SaveFirstTime(bool firstTime)
    {
        PlayerData data = LoadNew();
        data.setFirstTime(firstTime);
        //new PlayerData(GetListOfCategories(), path, GetVolume(), GetTutorial(), GetLanguage(), firstTime);

        return SaveData(data);
    }

    public static PlayerData LoadNew()
    {
        PlayerData data = Load();
        if (data == null) data = new PlayerData(GetListOfCategories(), path, GetVolume(), GetTutorial(), GetLanguage(), GetFirstTime());
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