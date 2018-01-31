using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapLevelStart : MapElementColored {
    private float startX;
    private float startY;
    private float startRot;

    public MapLevelStart(GameObject obj) : base(obj)
    {
        LevelStart start = obj.GetComponent<LevelStart>();
        this.startX = start.startX;
        this.startY = start.startY;
        this.startRot = start.startRot;
    }

    public float GetStartX() { return this.startX; }
    public float GetStartY() { return this.startY; }
    public float GetStartRot() { return this.startRot; }
}
