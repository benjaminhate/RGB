using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapRay : MapObstacle {
        private float speed;
        private float range;
        private float timeStop;
        private int dir;

        public MapRay(GameObject obj) : base(obj)
        {
            ChangeObstacleType(MapObstacleType.Ray);
            var ray = obj.GetComponent<RayController>();
            speed = ray.speed;
            range = ray.range;
            timeStop = ray.timeStop;
            dir = ray.dir;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public float GetRange()
        {
            return range;
        }

        public float GetTimeStop()
        {
            return timeStop;
        }

        public int GetDir()
        {
            return dir;
        }
    }
}
