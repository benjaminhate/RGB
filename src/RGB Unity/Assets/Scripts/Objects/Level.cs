using System;

namespace Objects
{
	[Serializable]
	public class Level {

		public string name;
		public float timer;
		public bool completed;
		public bool blocked;
		public string sceneName;
		public int id;

		public Level(string name, string sceneName, float timer, bool completed, bool blocked, int id)
		{
			this.name = name;
			this.sceneName = sceneName;
			this.timer = timer;
			this.completed = completed;
			this.blocked = blocked;
			this.id = id;
		}
		
		public override string ToString()
		{
			return
				$"Level : \n name : {name}\n id : {id}\n completed : {completed}\n blocked : {blocked}\n scene name : {sceneName}\n timer : {timer}";
		}

		public Level Clone()
		{
			return new Level(name, sceneName, timer, completed, blocked, id);
		}
	}
}
