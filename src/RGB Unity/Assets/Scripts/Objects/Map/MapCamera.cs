using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapCamera : MapObstacle
    {
        public List<CameraController.TargetAngle> targetAngles;
        public float rotSpeed;
        public float timeStop;
        public CameraController.CameraDirection direction;

        public MapCamera(GameObject obj) : base(obj)
        {
            obstacleType = MapObstacleType.Camera;
            var camera = obj.GetComponent<CameraController>();
            targetAngles = camera.targetAngles;
            rotSpeed = camera.rotSpeed;
            timeStop = camera.timeStop;
            direction = camera.initCameraDirection;
        }
    }
}
