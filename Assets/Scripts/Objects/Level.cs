using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Level {

	public string name;
	public float timer;
	public bool completed;
	public bool blocked;
	public string sceneName;

	public Level(string name,float timer,bool completed,bool blocked){
		this.name = name;
		this.timer = timer;
		this.completed = completed;
		this.blocked = blocked;
	}

	public Level setName(string name){
		this.name = name;
		return this;
	}
	public string getName(){
		return this.name;
	}

	public Level setTimer(float timer){
		this.timer = timer;
		return this;
	}
	public float getTimer(){
		return this.timer;
	}

	public Level setCompleted(bool completed){
		this.completed = completed;
		return this;
	}
	public bool getCompleted(){
		return this.completed;
	}

	public Level setBlocked(bool blocked){
		this.blocked = blocked;
		return this;
	}
	public bool getBlocked(){
		return this.blocked;
	}

	public Level setSceneName(string sceneName){
		this.sceneName = sceneName;
		return this;
	}
	public string getSceneName(){
		return this.sceneName;
	}

	public string toString() {
		return "Level : \n name : " 
			+ this.name 
			+ "\n completed : " 
			+ this.completed.ToString () 
			+ "\n blocked : " 
			+ this.blocked.ToString () 
			+ "\n scene name : " 
			+ this.sceneName;
	}
}
