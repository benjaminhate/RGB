using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        PlayerController player = obj.GetComponent<PlayerController>();
        this.speed = player.speed;
        this.decceleration = player.decceleration;
        this.dead = player.dead;
        this.respawn = player.respawn;
        this.finish = player.finish;
        this.obstacleCollide = player.obstacleCollide;
        this.obstacleKill = player.obstacleKill;
        this.obstacle = player.obstacle;
    }

    public float GetSpeed() { return this.speed; }
    public float GetDecceleration() { return this.decceleration; }
    public bool GetDead() { return this.dead; }
    public bool GetRespawn() { return this.respawn; }
    public bool GetFinish() { return this.finish; }
    public bool GetObstacleCollide() { return this.obstacleCollide; }
    public bool GetObstacleKill() { return this.obstacleKill; }
    public string GetObstacle() { return this.obstacle; }
}
