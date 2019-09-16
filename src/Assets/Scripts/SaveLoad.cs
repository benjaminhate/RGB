using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using Objects;

public class SaveLoad {
	private static readonly string Path = System.IO.Path.Combine(Application.persistentDataPath,"save.gd");

	private static Level SearchLevelNameInSave(List<Category> categories,string levelName){
        Debug.Log("Level name : " + levelName);
		foreach (var level in categories.SelectMany(category => category.GetLevels()))
		{
			Debug.Log("Level in Save : " + level.GetSceneName());
			if (string.CompareOrdinal (level.GetSceneName (), levelName) == 0)
				return level;
		}
		return null;
	}

	private static Category SearchLevelNameInCategory(List<Category> categories,string levelName)
	{
		return (from category in categories from level in category.GetLevels() where string.CompareOrdinal(level.GetSceneName(), levelName) == 0 select category).FirstOrDefault();
	}

	private static List<Category> GetListOfCategories ()
	{
		var data = Load ();
		return data?.categories ?? new List<Category> ();
	}

	private static bool GetVolume ()
	{
		var data = Load ();
		return data == null || data.volume;
	}

    private static bool GetTutorial()
    {
	    var data = Load();
	    return data != null && data.tutorial;
    }

    private static string GetLanguage()
    {
	    var data = Load();
	    return data != null ? data.language : "English";
    }

    private static bool GetFirstTime()
    {
	    var data = Load();
	    return data == null || data.firstTime;
    }

    private static void SetAllCompletedCategories(List<Category> categories)
    {
	    foreach (var category in categories.Where(category => category.GetCompletedLevels ().Count == category.GetLevels ().Count))
	    {
		    category.SetCompleted (true);
	    }
    }

    private static PlayerData SaveData(PlayerData data)
    {
        var bf = new BinaryFormatter();

        var file = !File.Exists(Path) ? File.Create(Path) : File.Open(Path, FileMode.Open);

        bf.Serialize(file, data);
        file.Close();

        return data;
    }

	public static PlayerData SaveInit(List<Category> categories){

        var data = LoadNew();
		data.SetCategories (categories);

        return SaveData(data);
		
	}

    public static PlayerData Save(PlayerData data)
    {
        return SaveData(data);
    }

	public static PlayerData SaveLevel(string levelName) {
		var categories = GetListOfCategories ();
		var level = SearchLevelNameInSave (categories, levelName);
		level?.SetBlocked (false);

		var category = SearchLevelNameInCategory (categories, levelName);
		category?.SetBlocked (false);
		SetAllCompletedCategories (categories);

        var data = LoadNew();
        data.SetCategories(categories);
        //new PlayerData(categories, path, GetVolume(), GetTutorial(), GetLanguage(), GetFirstTime());

        return SaveData(data);
	}

	public static PlayerData SaveTimer(float timer,string levelName){
        Debug.Log("Save Timer : " + timer);
		//string levelName = SceneManager.GetActiveScene ().name;
		var categories = GetListOfCategories ();
		var level = SearchLevelNameInSave (categories, levelName);
		if (level != null) {
			level.SetCompleted (true);
			level.SetBlocked (false);
			if (timer < level.GetTimer () || level.GetTimer () == 0)
				level.SetTimer (timer);
		}

		SetAllCompletedCategories (categories);

        var data = LoadNew();
        data.SetCategories(categories);
        //new PlayerData (categories,path,GetVolume(),GetTutorial(),GetLanguage(),GetFirstTime());

        return SaveData(data);
    }

	public static PlayerData SaveVolume(bool volume){

        var data = LoadNew();
        data.SetVolume(volume);
        //new PlayerData (GetListOfCategories(), path, volume, GetTutorial(), GetLanguage(), GetFirstTime());

        return SaveData(data);
    }

    public static PlayerData SaveTutorial(bool tutorial)
    {
        var data = LoadNew();
        data.SetTutorial(tutorial);
        //new PlayerData(GetListOfCategories(), path, GetVolume(), tutorial, GetLanguage(), GetFirstTime());

        return SaveData(data);
    }

    public static PlayerData SaveLanguage(string language)
    {
        var data = LoadNew();
        data.SetLanguage(language);
        //new PlayerData(GetListOfCategories(), path, GetVolume(), GetTutorial(), language, GetFirstTime());

        return SaveData(data);
    }

    public static PlayerData SaveFirstTime(bool firstTime)
    {
        var data = LoadNew();
        data.SetFirstTime(firstTime);
        //new PlayerData(GetListOfCategories(), path, GetVolume(), GetTutorial(), GetLanguage(), firstTime);

        return SaveData(data);
    }

    public static PlayerData LoadNew()
    {
        var data = Load() ?? new PlayerData(GetListOfCategories(), Path, GetVolume(), GetTutorial(), GetLanguage(), GetFirstTime());
        return data;
    }

    public static PlayerData Load() {
		if (File.Exists (Path)) {
			var bf = new BinaryFormatter ();
			var file = File.Open (Path, FileMode.Open);
			var data = (PlayerData)bf.Deserialize (file);
			file.Close ();
			return data;
		} else {
			return null;
		}
	}
}