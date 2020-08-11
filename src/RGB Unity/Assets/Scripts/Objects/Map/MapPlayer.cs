using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapPlayer : MapElementColored {
        public float speed;
        public float decceleration;
        public bool dead;
        public bool respawn;
        public bool finish;
        public bool obstacleCollide;
        public bool obstacleKill;
        public string obstacle;

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
    }
}
