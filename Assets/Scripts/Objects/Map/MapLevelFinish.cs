using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapLevelFinish : MapElement {
    private string nextLevel;

    public MapLevelFinish(GameObject obj) : base(obj)
    {
        LevelFinish finish = obj.GetComponent<LevelFinish>();
        this.nextLevel = finish.nextLevel;
    }

    public string GetNextLevel()
    {
        return this.nextLevel;
    }
}
