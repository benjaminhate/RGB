using ScriptableObjects;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

namespace Objects
{
    public class ColorElement : MonoBehaviour{
        public ColorScriptableObject colorSo;
        public bool canColorLight = true;

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

            var colorLight = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();
            if (colorLight == null || !canColorLight) return;
            colorLight.color = Color;
        }

        private void Start()
        {
            ChangeColor(colorSo);
        }
    }
}
