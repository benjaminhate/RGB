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

		public Level SetName(string name){
			this.name = name;
			return this;
		}
		public string GetName(){
			return name;
		}

		public Level SetId(int id)
		{
			this.id = id;
			return this;
		}

		public int GetId()
		{
			return id;
		}

		public Level SetTimer(float timer){
			this.timer = timer;
			return this;
		}
		public float GetTimer(){
			return timer;
		}

		public Level SetCompleted(bool completed){
			this.completed = completed;
			return this;
		}
		public bool GetCompleted(){
			return completed;
		}

		public Level SetBlocked(bool blocked){
			this.blocked = blocked;
			return this;
		}
		public bool GetBlocked(){
			return blocked;
		}

		public Level SetSceneName(string sceneName){
			this.sceneName = sceneName;
			return this;
		}
		public string GetSceneName(){
			return sceneName;
		}

		public string ToString() {
			return "Level : \n name : " 
			       + name
			       + "\n id : "
			       + id.ToString()
			       + "\n completed : " 
			       + completed.ToString () 
			       + "\n blocked : " 
			       + blocked.ToString () 
			       + "\n scene name : " 
			       + sceneName
			       + "\n timer : "
			       + timer;
		}

		public Level Clone()
		{
			return new Level(name, sceneName, timer, completed, blocked, id);
		}
	}
}
