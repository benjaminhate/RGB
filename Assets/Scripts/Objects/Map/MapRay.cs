using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapRay : MapObstacle {
    private float speed;
    private float range;
    private float timeStop;
    private int dir;

    public MapRay(GameObject obj) : base(obj)
    {
        base.ChangeObstacleType(MapObstacleType.RAY);
        RayController ray = obj.GetComponent<RayController>();
        this.speed = ray.speed;
        this.range = ray.range;
        this.timeStop = ray.timeStop;
        this.dir = ray.dir;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public float GetRange()
    {
        return this.range;
    }

    public float GetTimeStop()
    {
        return this.timeStop;
    }

    public int GetDir()
    {
        return this.dir;
    }
}
