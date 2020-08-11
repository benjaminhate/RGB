using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapLevelStart : MapElementColored {
        public float startX;
        public float startY;
        public float startRot;

        public MapLevelStart(GameObject obj) : base(obj)
        {
            var start = obj.GetComponent<LevelStart>();
            startX = start.startX;
            startY = start.startY;
            startRot = start.startRot;
        }
    }
}
