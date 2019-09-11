using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Objects.Map;

public class MapSaveLoad : MonoBehaviour {

    public static string defaultPath = Path.Combine(Application.persistentDataPath,"maps");

    public static MapData SaveMap(MapData data)
    {
        var path = Path.Combine(defaultPath, data.GetLevelName() + ".bytes");
        var bf = new BinaryFormatter();

        var file = !File.Exists(path) ? File.Create(path) : File.Open(path, FileMode.Open);

        bf.Serialize(file, data);
        file.Close();

        return data;
    }

    public static MapData SaveMapFromScene()
    {
        var mapData = new MapData(SceneManager.GetActiveScene().name);
        var rootGameObjects = new List<GameObject>();
        var scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootGameObjects);
        foreach(var obj in rootGameObjects)
        {
            Debug.Log(obj.name);
            if (obj.CompareTag("Wall"))
            {
                var wall = new MapElement(obj);
                wall.ChangeType(MapElement.MapElementType.Wall);
                mapData.AddElement(wall);
            }
            if (obj.CompareTag("Colorer"))
            {
                var colorer = new MapElementColored(obj);
                colorer.ChangeType(MapElement.MapElementType.Colorer);
                mapData.AddElement(colorer);
            }
            if (obj.CompareTag("Game"))
            {
                var game = new MapGame(obj);
                mapData.AddElement(game);
            }
            if (obj.GetComponent<RayController>()!=null)
            {
                var ray = new MapRay(obj);
                mapData.AddElement(ray);
            }
            if (obj.GetComponent<CameraController>()!=null)
            {
                var camera = new MapCamera(obj);
                mapData.AddElement(camera);
            }
            if (obj.GetComponentInChildren<DetectorController>()!=null)
            {
                var detector = new MapDetector(obj);
                mapData.AddElement(detector);
            }
            if (obj.GetComponent<PlayerController>()!=null)
            {
                var player = new MapPlayer(obj);
                mapData.AddElement(player);
            }
            if (obj.GetComponent<LevelStart>()!=null)
            {
                var levelStart = new MapLevelStart(obj);
                mapData.AddElement(levelStart);
            }
            if (obj.GetComponent<LevelFinish>()!=null)
            {
                var levelFinish = new MapLevelFinish(obj);
                mapData.AddElement(levelFinish);
            }
        }
        return SaveMap(mapData);
    }

    public static MapData LoadMap(string levelName)
    {
        var path = Path.Combine(defaultPath, levelName + ".bytes");
        Debug.Log(path);
        if (File.Exists(path))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(path, FileMode.Open);
            var mapData = (MapData)bf.Deserialize(file);
            file.Close();
            return mapData;
        }
        else
        {
            return null;
        }
    }
}
