using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapElement {
    private float posX;
    private float posY;
    private float rotZ;
    private float scaleX;
    private float scaleY;

    public enum MapElementType { WALL, BACKGROUND, PLAYER, LEVELSTART
            , LEVELFINISH, OBSTACLE, COLORER, GAME };
    private MapElementType type;

    public MapElement(GameObject obj)
    {
        this.posX = obj.transform.position.x;
        this.posY = obj.transform.position.y;
        this.rotZ = obj.transform.rotation.eulerAngles.z;
        this.scaleX = obj.transform.localScale.x;
        this.scaleY = obj.transform.localScale.y;
    }

    public MapElement()
    {
        ChangeType(MapElementType.WALL);
        this.posX = 0;
        this.posY = 0;
        this.rotZ = 0;
        this.scaleX = 0;
        this.scaleY = 0;
    }

    public void ChangeType(MapElementType type)
    {
        this.type = type;
    }

    public MapElementType GetElementType()
    {
        return this.type;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(this.posX, this.posY);
    }

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(0,0,this.rotZ);
    }

    public Vector2 GetScale()
    {
        return new Vector2(this.scaleX, this.scaleY);
    }
}
