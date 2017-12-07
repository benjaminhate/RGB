using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Category {

	public string name;
	public List<Level> levels;
	public bool completed;
	public bool blocked;
    public int id;

	public Category(string name,List<Level> levels,bool completed,bool blocked,int id) {
		this.name = name;
		this.levels = levels;
		this.completed = completed;
		this.blocked = blocked;
        this.id = id;
	}

	public Category setName(string name) {
		this.name = name;
		return this;
	}

	public string getName() {
		return this.name;
	}

    public Category setId(int id)
    {
        this.id = id;
        return this;
    }

    public int getId()
    {
        return this.id;
    }

	public Category setCompleted(bool completed){
		this.completed = completed;
		return this;
	}
	public bool getCompleted(){
		return this.completed;
	}

	public Category setBlocked(bool blocked){
		this.blocked = blocked;
		return this;
	}
	public bool getBlocked(){
		return this.blocked;
	}

	public Category setLevels(List<Level> levels) {
		this.levels = levels;
		return this;
	}

	public List<Level> getLevels() {
		return this.levels;
	}

	public List<Level> getCompletedLevels() {
		List<Level> levels = new List<Level> ();
		foreach (Level level in this.levels) {
			if (level.completed) {
				levels.Add (level);
			}
		}
		return levels;
	}

	public List<Level> getUnblockedLevels() {
		List<Level> levels = new List<Level> ();
		foreach (Level level in this.levels) {
			if (!level.blocked) {
				levels.Add (level);
			}
		}
		return levels;
	}

	public List<Level> getBlockedLevels() {
		List<Level> levels = new List<Level> ();
		foreach (Level level in this.levels) {
			if (level.blocked) {
				levels.Add (level);
			}
		}
		return levels;
	}

    public Level getLevelWithName(String name)
    {
        foreach(Level level in levels)
        {
            if (level.name == name)
            {
                return level;
            }
        }
        return null;
    }

    public Level getLevelWithId(int id)
    {
        foreach(Level level in levels)
        {
            if (level.id == id)
            {
                return level;
            }
        }
        return null;
    }

	public string toString() {
        string sb = "Category : \n name : "
                    + this.name
                    + "\n id : "
                    + this.id.ToString()
		            + "\n completed : "
		            + this.completed.ToString ()
		            + "\n blocked : "
		            + this.blocked.ToString ()
		            + "\n levels : [";
		foreach(Level level in this.levels) {
			sb += "\n" + level.toString ();
		}
		return sb + "\n]";
	}
}
