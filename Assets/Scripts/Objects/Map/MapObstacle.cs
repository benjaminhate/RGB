using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapObstacle : MapElementColored {
    public enum MapObstacleType { DETECTOR, CAMERA, RAY };
    private MapObstacleType obstacleType;

    public MapObstacle(GameObject obj) : base(obj)
    {
        base.ChangeType(MapElementType.OBSTACLE);
    }

    public MapObstacle() : base()
    {

    }

    public void ChangeObstacleType(MapObstacleType type)
    {
        this.obstacleType = type;
    }

    public MapObstacleType GetObstacleType()
    {
        return this.obstacleType;
    }
}
