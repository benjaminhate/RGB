using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapData {
    private List<MapElement> elements;
    private string levelName;

    public MapData()
    {
        this.elements = new List<MapElement>();
        this.levelName = "";
    }

    public MapData(string levelName)
    {
        this.elements = new List<MapElement>();
        this.levelName = levelName;
    }

    public List<MapElement> GetElements() { return elements; }
    public string GetLevelName() { return this.levelName; }

    public void AddElement(MapElement element)
    {
        this.elements.Add(element);
    }
}
