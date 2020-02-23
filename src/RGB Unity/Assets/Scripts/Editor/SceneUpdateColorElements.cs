using System;
using Objects;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    [InitializeOnLoad]
    public class SceneUpdateColorElements
    {
        static SceneUpdateColorElements()
        {
            EditorApplication.update += RunOnce;
        }

        private static void RunOnce()
        {
            UpdateColorElements();
            EditorApplication.update -= RunOnce;
        }

        [MenuItem("Tools/Update Color Elements")]
        public static void MenuUpdateColorElements()
        {
            UpdateColorElements();
        }

        private static void UpdateColorElements()
        {
            var colorElements = Object.FindObjectsOfType<ColorElement>();
            foreach (var colorElement in colorElements)
            {
                ColorElementEditor.RefreshColor(colorElement);
            }
        }
    }
}