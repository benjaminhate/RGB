using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "RGB/Color")]
    public class ColorScriptableObject : ScriptableObject
    {
        public Color color;
    }
}