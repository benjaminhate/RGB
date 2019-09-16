using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapLevelFinish : MapElement {
        private string nextLevel;

        public MapLevelFinish(GameObject obj) : base(obj)
        {
            var finish = obj.GetComponent<LevelFinish>();
            nextLevel = finish.nextLevel;
        }

        public string GetNextLevel()
        {
            return nextLevel;
        }
    }
}
