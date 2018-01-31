using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapGame : MapElement {

    private MapElement background;
    private MapPlayer player;
    private MapLevelFinish levelFinish;
    private MapLevelStart levelStart;

    private GameObject GetChildWithTag(GameObject obj, string tag)
    {
        GameObject child;
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            child = obj.transform.GetChild(i).gameObject;
            if (child.CompareTag(tag))
                return child;
        }
        return null;
    }

    public MapGame(GameObject obj) : base(obj)
    {
        base.ChangeType(MapElementType.GAME);
        this.background = new MapElement(GetChildWithTag(obj,"MainBackground"));
        this.player = new MapPlayer(obj.GetComponentInChildren<PlayerController>().gameObject);
        this.levelFinish = new MapLevelFinish(obj.GetComponentInChildren<LevelFinish>().gameObject);
        this.levelStart = new MapLevelStart(obj.GetComponentInChildren<LevelStart>().gameObject);
    }

    public MapElement GetBackground() { return this.background; }
    public MapPlayer GetPlayer() { return this.player; }
    public MapLevelFinish GetLevelFinish() { return this.levelFinish; }
    public MapLevelStart GetLevelStart() { return this.levelStart; }
}
