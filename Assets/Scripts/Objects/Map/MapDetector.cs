using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapDetector : MapObstacle {
    private float speed;
    private float timeStop;

    public MapDetector(GameObject obj) : base(obj)
    {
        base.ChangeObstacleType(MapObstacleType.DETECTOR);
        base.ChangeColorType(obj.GetComponentInChildren<DetectorController>().gameObject.GetComponent<ColorElement>().GetColor());
        DetectorController detector = obj.GetComponentInChildren<DetectorController>();
        this.speed = detector.speed;
        this.timeStop = detector.timeStop;
    }

    public float GetSpeed() { return this.speed; }
    public float GetTimeStop() { return this.timeStop; }
}
