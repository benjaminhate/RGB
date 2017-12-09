using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData {

	public List<Category> categories;
	public string path;
	public bool volume;
    public bool tutorial;

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

    public Category getCategoryWithName(String name)
    {
        foreach(Category category in categories)
        {
            if (category.getName() == name)
            {
                return category;
            }
        }
        return null;
    }

    public Category getCategoryWithId(int id)
    {
        foreach(Category category in categories)
        {
            if (category.id == id)
            {
                return category;
            }
        }
        return null;
    }

	public PlayerData setVolume(bool volume){
		this.volume = volume;
		return this;
	}
	public bool getVolume(){
		return this.volume;
	}

    public PlayerData setTutorial(bool tutorial)
    {
        this.tutorial = tutorial;
        return this;
    }
    public bool getTutorial()
    {
        return this.tutorial;
    }

    public String toString()
    {
        String sb = "PlayerData : "
            + "\n volume : "
            + this.volume.ToString()
            + "\n path : "
            + this.path;
        foreach(Category category in categories)
        {
            sb += "\n" + category.toString();
        }
        return sb;
    }
}
