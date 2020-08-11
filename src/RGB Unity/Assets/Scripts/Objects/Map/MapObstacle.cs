using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapObstacle : MapElementColored {
        public enum MapObstacleType { Detector, Camera, Ray };
        public MapObstacleType obstacleType;

        public MapObstacle(GameObject obj) : base(obj)
        {
            type = MapElementType.Obstacle;
        }

        public MapObstacle() : base()
        {

        }
    }
}
