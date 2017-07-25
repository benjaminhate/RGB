using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData {

	public List<Category> categories;
	public string path;
	public bool volume;

	public PlayerData setPath(string path){
		this.path = path;
		return this;
	}
	public string getPath(){
		return this.path;
	}

	public PlayerData setCategories(List<Category> categories){
		this.categories = categories;
		return this;
	}
	public List<Category> getCategories(){
		return this.categories;
	}

	public PlayerData setVolume(bool volume){
		this.volume = volume;
		return this;
	}
	public bool getVolume(){
		return this.volume;
	}
}
