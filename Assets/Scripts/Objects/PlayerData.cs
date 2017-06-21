﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData {

	public List<Scene> scenes;
	public string path;

	public PlayerData setPath(string path){
		this.path = path;
		return this;
	}
	public string getPath(){
		return this.path;
	}

	public PlayerData setScenes(List<Scene> scenes){
		this.scenes = scenes;
		return this;
	}
	public List<Scene> getScenes(){
		return this.scenes;
	}
	public PlayerData addScenesItem(Scene scene){
		this.scenes.Add (scene);
		return this;
	}
}