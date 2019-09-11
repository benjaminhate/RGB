using System;

namespace Objects
{
	[Serializable]
	public class Scene {

		public string name;
		public float timer;

		public Scene(string name,float timer){
			this.name = name;
			this.timer = timer;
		}

		public Scene SetName(string name){
			this.name = name;
			return this;
		}
		public string GetName(){
			return name;
		}

		public Scene SetTimer(float timer){
			this.timer = timer;
			return this;
		}
		public float GetTimer(){
			return timer;
		}
	}
}
