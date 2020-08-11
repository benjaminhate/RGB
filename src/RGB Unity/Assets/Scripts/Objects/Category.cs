using System;
using System.Collections.Generic;
using System.Linq;

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

		public List<Level> GetCompletedLevels()
		{
			return levels.Where(level => level.completed).ToList();
		}

		public List<Level> GetUnblockedLevels()
		{
			return levels.Where(level => !level.blocked).ToList();
		}

		public List<Level> GetBlockedLevels()
		{
			return levels.Where(level => level.blocked).ToList();
		}

		public Level GetLevelWithName(string levelName)
		{
			return levels.FirstOrDefault(level => level.name == levelName);
		}

		public Level GetLevelWithId(int levelId)
		{
			return levels.FirstOrDefault(level => level.id == levelId);
		}

		public override string ToString()
		{
			var sb =
				$"Category : \n name : {name}\n id : {id}\n completed : {completed}\n blocked : {blocked}\n levels : [";
			sb = levels.Aggregate(sb, (current, level) => current + ($"\n{level}"));
			return sb + "\n]";
		}

		public Category Clone()
		{
			var cloneLevels = levels.Select(lvl => lvl.Clone()).ToList();
			return new Category(name, cloneLevels, completed, blocked, id);
		}
	}
}
