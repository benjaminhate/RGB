using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapCamera : MapObstacle {
        private int degA;
        private int degB;
        private int rotSpeed;
        private float timeStop;
        private int dir;

        public MapCamera(GameObject obj) : base(obj)
        {
            ChangeObstacleType(MapObstacleType.Camera);
            var camera = obj.GetComponent<CameraController>();
            degA = camera.degA;
            degB = camera.degB;
            rotSpeed = camera.rotSpeed;
            timeStop = camera.timeStop;
            dir = camera.dir;
        }

        public int GetDegA() { return degA; }
        public int GetDegB() { return degB; }
        public int GetRotSpeed() { return rotSpeed; }
        public float GetTimeStop() { return timeStop; }
        public int GetDir() { return dir; }
    }
}
