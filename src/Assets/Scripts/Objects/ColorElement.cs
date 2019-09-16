using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class ColorElement : MonoBehaviour{
        public ColorScriptableObject colorSo;

        public Color Color => colorSo.color;

        public bool SameColor(ColorScriptableObject other)
        {
            return colorSo.color == other.color;
        }

        public void ChangeColor(ColorScriptableObject other)
        {
            colorSo = other;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;

            spriteRenderer.color = Color;
        }

        private void Start()
        {
            ChangeColor(colorSo);
        }
    }
}
