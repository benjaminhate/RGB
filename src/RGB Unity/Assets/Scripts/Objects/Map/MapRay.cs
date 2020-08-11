using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapRay : MapObstacle {
        private float _speed;
        private float _range;
        private float _timeStop;
        private int _dir;

        public MapRay(GameObject obj) : base(obj)
        {
            obstacleType = MapObstacleType.Ray;
            var ray = obj.GetComponent<RayController>();
            _speed = ray.speed;
            _range = ray.range;
            _timeStop = ray.timeStop;
            _dir = ray.dir;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public float GetRange()
        {
            return _range;
        }

        public float GetTimeStop()
        {
            return _timeStop;
        }

        public int GetDir()
        {
            return _dir;
        }
    }
}
