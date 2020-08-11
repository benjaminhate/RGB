using System;
using ScriptableObjects;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapElementColored : MapElement {
        // Colorer, LevelStart, Obstacles
        public ColorScriptableObject ColorSo { get; private set; }

        public MapElementColored(GameObject obj) : base(obj)
        {
            var element = obj.GetComponent<ColorElement>();
            if(element != null)
                ColorSo = element.colorSo;
        }

        public MapElementColored() : base()
        {

        }

        public void ChangeColor(ColorScriptableObject other)
        {
            ColorSo = other;
        }
    }
}
