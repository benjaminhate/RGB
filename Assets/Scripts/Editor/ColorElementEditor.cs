using System;
using Objects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ColorElement))]
    public class ColorElementEditor : UnityEditor.Editor
    {
        private ColorElement colorElement;

        private void Awake()
        {
            colorElement = (ColorElement) target;
        }

        public void OnSceneGUI()
        {
            var spriteRenderer = colorElement.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;

            spriteRenderer.color = colorElement.colorSo == null ? Color.white : colorElement.colorSo.color;
        }
    }
}
