using System;
using System.Collections.Generic;

namespace Objects
{
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

		public Category SetName(string name) {
			this.name = name;
			return this;
		}

		public string GetName() {
			return name;
		}

		public Category SetId(int id)
		{
			this.id = id;
			return this;
		}

		public int GetId()
		{
			return id;
		}

		public Category SetCompleted(bool completed){
			this.completed = completed;
			return this;
		}
		public bool GetCompleted(){
			return completed;
		}

		public Category SetBlocked(bool blocked){
			this.blocked = blocked;
			return this;
		}
		public bool GetBlocked(){
			return blocked;
		}

		public Category SetLevels(List<Level> levels) {
			this.levels = levels;
			return this;
		}

		public List<Level> GetLevels() {
			return levels;
		}

		public List<Level> GetCompletedLevels() {
			var levels = new List<Level> ();
			foreach (var level in this.levels) {
				if (level.completed) {
					levels.Add (level);
				}
			}
			return levels;
		}

		public List<Level> GetUnblockedLevels() {
			var levels = new List<Level> ();
			foreach (var level in this.levels) {
				if (!level.blocked) {
					levels.Add (level);
				}
			}
			return levels;
		}

		public List<Level> GetBlockedLevels() {
			var levels = new List<Level> ();
			foreach (var level in this.levels) {
				if (level.blocked) {
					levels.Add (level);
				}
			}
			return levels;
		}

		public Level GetLevelWithName(String name)
		{
			foreach(var level in levels)
			{
				if (level.name == name)
				{
					return level;
				}
			}
			return null;
		}

		public Level GetLevelWithId(int id)
		{
			foreach(var level in levels)
			{
				if (level.id == id)
				{
					return level;
				}
			}
			return null;
		}

		public string ToString() {
			var sb = "Category : \n name : "
			            + name
			            + "\n id : "
			            + id.ToString()
			            + "\n completed : "
			            + completed.ToString ()
			            + "\n blocked : "
			            + blocked.ToString ()
			            + "\n levels : [";
			foreach(var level in levels) {
				sb += "\n" + level.ToString ();
			}
			return sb + "\n]";
		}

		public Category Clone()
		{
			var levels = new List<Level>();
			foreach(var lvl in this.levels)
			{
				levels.Add(lvl.Clone());
			}
			return new Category(name, levels, completed, blocked, id);
		}
	}
}
