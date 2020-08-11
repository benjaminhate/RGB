using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapLevelFinish : MapElement {
        public string nextLevel;

        public MapLevelFinish(GameObject obj) : base(obj)
        {
            var finish = obj.GetComponent<LevelFinish>();
            nextLevel = finish.nextLevel;
        }
    }
}
