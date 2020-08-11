using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapCamera : MapObstacle {
        public int degA;
        public int degB;
        public int rotSpeed;
        public float timeStop;
        public int dir;

        public MapCamera(GameObject obj) : base(obj)
        {
            obstacleType = MapObstacleType.Camera;
            var camera = obj.GetComponent<CameraController>();
            degA = camera.degA;
            degB = camera.degB;
            rotSpeed = camera.rotSpeed;
            timeStop = camera.timeStop;
            dir = camera.dir;
        }
    }
}
