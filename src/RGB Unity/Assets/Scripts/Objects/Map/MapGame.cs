using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapGame : MapElement {

        public MapElement background;
        public MapPlayer player;
        public MapLevelFinish levelFinish;
        public MapLevelStart levelStart;

        private GameObject GetChildWithTag(GameObject obj, string tag)
        {
            for(var i = 0; i < obj.transform.childCount; i++)
            {
                var child = obj.transform.GetChild(i).gameObject;
                if (child.CompareTag(tag))
                    return child;
            }
            return null;
        }

        public MapGame(GameObject obj) : base(obj)
        {
            type = MapElementType.Game;
            background = new MapElement(GetChildWithTag(obj,"MainBackground"));
            player = new MapPlayer(obj.GetComponentInChildren<PlayerController>().gameObject);
            levelFinish = new MapLevelFinish(obj.GetComponentInChildren<LevelFinish>().gameObject);
            levelStart = new MapLevelStart(obj.GetComponentInChildren<LevelStart>().gameObject);
        }
    }
}
