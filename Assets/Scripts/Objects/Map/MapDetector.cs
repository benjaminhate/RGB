using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapDetector : MapObstacle {
        private float speed;
        private float timeStop;

        public MapDetector(GameObject obj) : base(obj)
        {
            ChangeObstacleType(MapObstacleType.Detector);
            ChangeColor(obj.GetComponentInChildren<DetectorController>().gameObject.GetComponent<ColorElement>().Color);
            var detector = obj.GetComponentInChildren<DetectorController>();
            speed = detector.speed;
            timeStop = detector.timeStop;
        }

        public float GetSpeed() { return speed; }
        public float GetTimeStop() { return timeStop; }
    }
}
