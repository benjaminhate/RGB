using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapCamera : MapObstacle {
    private int degA;
    private int degB;
    private int rotSpeed;
    private float timeStop;
    private int dir;

    public MapCamera(GameObject obj) : base(obj)
    {
        base.ChangeObstacleType(MapObstacleType.CAMERA);
        CameraController camera = obj.GetComponent<CameraController>();
        this.degA = camera.degA;
        this.degB = camera.degB;
        this.rotSpeed = camera.rotSpeed;
        this.timeStop = camera.timeStop;
        this.dir = camera.dir;
    }

    public int GetDegA() { return this.degA; }
    public int GetDegB() { return this.degB; }
    public int GetRotSpeed() { return this.rotSpeed; }
    public float GetTimeStop() { return this.timeStop; }
    public int GetDir() { return this.dir; }
}
