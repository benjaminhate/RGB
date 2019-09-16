using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapObstacle : MapElementColored {
        public enum MapObstacleType { Detector, Camera, Ray };
        private MapObstacleType obstacleType;

        public MapObstacle(GameObject obj) : base(obj)
        {
            ChangeType(MapElementType.Obstacle);
        }

        public MapObstacle() : base()
        {

        }

        public void ChangeObstacleType(MapObstacleType type)
        {
            obstacleType = type;
        }

        public MapObstacleType GetObstacleType()
        {
            return obstacleType;
        }
    }
}
