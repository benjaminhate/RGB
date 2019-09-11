using ScriptableObjects;
using UnityEngine;

namespace Objects
{
    public class ColorElement : MonoBehaviour{
        public ColorScriptableObject colorSo;

        private readonly Color red = new Color(1f, 0.2f, 0.2f, 1f);
        private readonly Color green = new Color(0f, 0.8f, 0f, 1f);
        private readonly Color blue = new Color(0.1f, 0.3f, 1f, 1f);

        public Color Color { get; private set; }

        public bool SameColor(ColorScriptableObject other)
        {
            return colorSo.color == other.color;
        }

        public void ChangeColor(Color other)
        {
            Color = other;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;

            spriteRenderer.color = Color;
        }

        private void Start()
        {
            ChangeColor(colorSo.color);
        }
    }
}
