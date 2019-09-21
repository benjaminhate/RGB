using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapLevelStart : MapElementColored {
        private float startX;
        private float startY;
        private float startRot;

        public MapLevelStart(GameObject obj) : base(obj)
        {
            var start = obj.GetComponent<LevelStart>();
            startX = start.startX;
            startY = start.startY;
            startRot = start.startRot;
        }

        public float GetStartX() { return startX; }
        public float GetStartY() { return startY; }
        public float GetStartRot() { return startRot; }
    }
}
