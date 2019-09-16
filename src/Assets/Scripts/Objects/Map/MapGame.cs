using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapGame : MapElement {

        private MapElement background;
        private MapPlayer player;
        private MapLevelFinish levelFinish;
        private MapLevelStart levelStart;

        private GameObject GetChildWithTag(GameObject obj, string tag)
        {
            GameObject child;
            for(var i = 0; i < obj.transform.childCount; i++)
            {
                child = obj.transform.GetChild(i).gameObject;
                if (child.CompareTag(tag))
                    return child;
            }
            return null;
        }

        public MapGame(GameObject obj) : base(obj)
        {
            ChangeType(MapElementType.Game);
            background = new MapElement(GetChildWithTag(obj,"MainBackground"));
            player = new MapPlayer(obj.GetComponentInChildren<PlayerController>().gameObject);
            levelFinish = new MapLevelFinish(obj.GetComponentInChildren<LevelFinish>().gameObject);
            levelStart = new MapLevelStart(obj.GetComponentInChildren<LevelStart>().gameObject);
        }

        public MapElement GetBackground() { return background; }
        public MapPlayer GetPlayer() { return player; }
        public MapLevelFinish GetLevelFinish() { return levelFinish; }
        public MapLevelStart GetLevelStart() { return levelStart; }
    }
}
