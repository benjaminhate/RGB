using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MainCameraController))]
    public class MainCameraControllerEditor : UnityEditor.Editor
    {
        [MenuItem("Tools/Update Main Camera Controller")]
        public static void MenuUpdateColorElements()
        {
            var cameras = FindObjectsOfType<MainCameraController>();
            foreach (var camera in cameras)
            {
                RefreshPosition(camera);
            }
        }
        
        public void OnSceneGUI()
        {
            RefreshPosition((MainCameraController) target);
        }

        public override void OnInspectorGUI()
        {
            RefreshPosition((MainCameraController) target);
            base.OnInspectorGUI();
        }

        private static void RefreshPosition(MainCameraController mainCameraController)
        {
            var player = mainCameraController.player;
            if (player == null) return;

            var playerPosition = player.transform.position;
            mainCameraController.gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y, -10f);
        }
    }
}