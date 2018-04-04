using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MapSaveLoad : MonoBehaviour {

    public static string defaultPath = Path.Combine(Application.persistentDataPath,"maps");

    public static MapData SaveMap(MapData data)
    {
        string path = Path.Combine(defaultPath, data.GetLevelName() + ".bytes");
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

    public static MapData SaveMapFromScene()
    {
        MapData mapData = new MapData(SceneManager.GetActiveScene().name);
        List<GameObject> rootGameObjects = new List<GameObject>();
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootGameObjects);
        foreach(GameObject obj in rootGameObjects)
        {
            Debug.Log(obj.name);
            if (obj.CompareTag("Wall"))
            {
                MapElement wall = new MapElement(obj);
                wall.ChangeType(MapElement.MapElementType.WALL);
                mapData.AddElement(wall);
            }
            if (obj.CompareTag("Colorer"))
            {
                MapElementColored colorer = new MapElementColored(obj);
                colorer.ChangeType(MapElement.MapElementType.COLORER);
                mapData.AddElement(colorer);
            }
            if (obj.CompareTag("Game"))
            {
                MapGame game = new MapGame(obj);
                mapData.AddElement(game);
            }
            if (obj.GetComponent<RayController>()!=null)
            {
                MapRay ray = new MapRay(obj);
                mapData.AddElement(ray);
            }
            if (obj.GetComponent<CameraController>()!=null)
            {
                MapCamera camera = new MapCamera(obj);
                mapData.AddElement(camera);
            }
            if (obj.GetComponentInChildren<DetectorController>()!=null)
            {
                MapDetector detector = new MapDetector(obj);
                mapData.AddElement(detector);
            }
            if (obj.GetComponent<PlayerController>()!=null)
            {
                MapPlayer player = new MapPlayer(obj);
                mapData.AddElement(player);
            }
            if (obj.GetComponent<LevelStart>()!=null)
            {
                MapLevelStart levelStart = new MapLevelStart(obj);
                mapData.AddElement(levelStart);
            }
            if (obj.GetComponent<LevelFinish>()!=null)
            {
                MapLevelFinish levelFinish = new MapLevelFinish(obj);
                mapData.AddElement(levelFinish);
            }
        }
        return SaveMap(mapData);
    }

    public static MapData LoadMap(string levelName)
    {
        string path = Path.Combine(defaultPath, levelName + ".bytes");
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            MapData mapData = (MapData)bf.Deserialize(file);
            file.Close();
            return mapData;
        }
        else
        {
            return null;
        }
    }
}
