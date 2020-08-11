using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapDetector : MapObstacle {
        public float speed;
        public float timeStop;

        public MapDetector(GameObject obj) : base(obj)
        {
            obstacleType = MapObstacleType.Detector;
            ChangeColor(obj.GetComponentInChildren<DetectorController>().gameObject.GetComponent<ColorElement>().colorSo);
            var detector = obj.GetComponentInChildren<DetectorController>();
            speed = detector.speed;
            timeStop = detector.timeStop;
        }
    }
}
