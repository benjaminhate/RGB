﻿using UnityEngine;
using System.IO;

public class FirstTimeLaunch {

    public static void LoadAll()
    {
        LoadMaps();
        LoadLanguages();
    }

    public static void LoadMaps()
    {
        var maps = Resources.LoadAll<TextAsset>("maps");
        if (!Directory.Exists(MapSaveLoad.defaultPath))
        {
            Directory.CreateDirectory(MapSaveLoad.defaultPath);
        }
        foreach(var map in maps)
        {
            File.WriteAllBytes(Path.Combine(MapSaveLoad.defaultPath, map.name + ".bytes"), map.bytes);
        }
    }

    public static void LoadLanguages()
    {
        var language = Resources.Load<TextAsset>("languages");
        File.WriteAllBytes(LanguageController.path, language.bytes);
    }
}
