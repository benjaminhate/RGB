using Objects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ColorElement))]
    public class ColorElementEditor : UnityEditor.Editor
    {
        public void OnSceneGUI()
        {
            RefreshColor((ColorElement) target);
        }

        public override void OnInspectorGUI()
        {
            RefreshColor((ColorElement) target);
            base.OnInspectorGUI();
        }

        public static void RefreshColor(ColorElement colorElement)
        {
            var color = colorElement.colorSo == null ? Color.white : colorElement.colorSo.color;
            
            var spriteRenderer = colorElement.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }

            var light = colorElement.GetComponentInChildren<Light2D>();
            if (light != null)
            {
                light.color = color;
            }
        }
    }
}
