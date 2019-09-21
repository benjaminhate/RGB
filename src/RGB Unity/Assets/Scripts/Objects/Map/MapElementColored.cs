using System;
using ScriptableObjects;
using UnityEngine;

namespace Objects.Map
{
    [Serializable]
    public class MapElementColored : MapElement {
        // Colorer, LevelStart, Obstacles
        public ColorScriptableObject ColorSO { get; private set; }

        public MapElementColored(GameObject obj) : base(obj)
        {
            var element = obj.GetComponent<ColorElement>();
            if(element != null)
                ColorSO = element.colorSo;
        }

        public MapElementColored() : base()
        {

        }

        public void ChangeColor(ColorScriptableObject other)
        {
            ColorSO = other;
        }
    }
}
