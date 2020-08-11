using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapElement {
        public float posX;
        public float posY;
        public float rotZ;
        public float scaleX;
        public float scaleY;

        public enum MapElementType { Wall, Background, Player, LevelStart
            , LevelFinish, Obstacle, Colorer, Game };
        public MapElementType type;

        public MapElement(GameObject obj)
        {
            posX = obj.transform.position.x;
            posY = obj.transform.position.y;
            rotZ = obj.transform.rotation.eulerAngles.z;
            scaleX = obj.transform.localScale.x;
            scaleY = obj.transform.localScale.y;
        }

        public MapElement()
        {
            type = MapElementType.Wall;
            posX = 0;
            posY = 0;
            rotZ = 0;
            scaleX = 0;
            scaleY = 0;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(posX, posY);
        }

        public Quaternion GetRotation()
        {
            return Quaternion.Euler(0,0,rotZ);
        }

        public Vector2 GetScale()
        {
            return new Vector2(scaleX, scaleY);
        }
    }
}
