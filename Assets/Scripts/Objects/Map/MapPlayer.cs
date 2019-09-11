using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapPlayer : MapElementColored {
        private float speed;
        private float decceleration;
        private bool dead;
        private bool respawn;
        private bool finish;
        private bool obstacleCollide;
        private bool obstacleKill;
        private string obstacle;

        public MapPlayer(GameObject obj) : base(obj)
        {
            var player = obj.GetComponent<PlayerController>();
            speed = player.speed;
            decceleration = player.decceleration;
            dead = player.dead;
            respawn = player.respawn;
            finish = player.finish;
            obstacleCollide = player.obstacleCollide;
            obstacleKill = player.obstacleKill;
            obstacle = player.obstacle;
        }

        public float GetSpeed() { return speed; }
        public float GetDecceleration() { return decceleration; }
        public bool GetDead() { return dead; }
        public bool GetRespawn() { return respawn; }
        public bool GetFinish() { return finish; }
        public bool GetObstacleCollide() { return obstacleCollide; }
        public bool GetObstacleKill() { return obstacleKill; }
        public string GetObstacle() { return obstacle; }
    }
}
