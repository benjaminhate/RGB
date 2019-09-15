using Objects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ColorElement))]
    public class ColorElementEditor : UnityEditor.Editor
    {
        public void OnSceneGUI()
        {
            var colorElement = (ColorElement) target;
            var spriteRenderer = colorElement.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;

            spriteRenderer.color = colorElement.colorSo == null ? Color.white : colorElement.colorSo.color;
        }
    }
}
