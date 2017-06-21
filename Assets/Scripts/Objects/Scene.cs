using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Scene {

	public string name;
	public float timer;

	public Scene(string name,float timer){
		this.name = name;
		this.timer = timer;
	}

	public Scene setName(string name){
		this.name = name;
		return this;
	}
	public string getName(){
		return this.name;
	}

	public Scene setTimer(float timer){
		this.timer = timer;
		return this;
	}
	public float getTimer(){
		return this.timer;
	}
}
