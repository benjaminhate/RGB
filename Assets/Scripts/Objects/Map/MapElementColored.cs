using System;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapElementColored : MapElement {
        // Colorer, LevelStart, Obstacles
        public Color Color { get; private set; }

        public MapElementColored(GameObject obj) : base(obj)
        {
            var color = obj.GetComponent<ColorElement>();
            if(color != null)
                Color = color.Color;
        }

        public MapElementColored() : base()
        {

        }

        public void ChangeColor(Color other)
        {
            Color = other;
        }
    }
}
