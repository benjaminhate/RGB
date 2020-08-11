namespace Editor
{
        using UnityEngine;
    using UnityEditor;
    using UnityEngine.UI;
    using TMPro;
     
    public class TextToTextMeshPro : Editor
    {
        public class TextMeshProSettings
        {
            public bool enabled;
            public FontStyles fontStyle;
            public float fontSize;
            public float fontSizeMin;
            public float fontSizeMax;
            public float lineSpacing;
            public bool enableRichText;
            public bool enableAutoSizing;
            public TextAlignmentOptions textAlignmentOptions;
            public bool wrappingEnabled;
            public TextOverflowModes textOverflowModes;
            public string text;
            public Color color;
            public bool rayCastTarget;
        }
     
        [MenuItem("Tools/Text To TextMeshPro", false, 4000)]
        static void DoIt()
        {
            if(TMPro.TMP_Settings.defaultFontAsset == null)
            {
                EditorUtility.DisplayDialog("ERROR!", "Assign a default font asset in project settings!", "OK", "");
                return;
            }
     
            foreach(GameObject gameObject in Selection.gameObjects)
            {
                ConvertTextToTextMeshPro(gameObject);
            }
        }
     
        static void ConvertTextToTextMeshPro(GameObject target)
        {
            TextMeshProSettings settings = GetTextMeshProSettings(target);
     
            DestroyImmediate(target.GetComponent<Text>());
     
            TextMeshProUGUI tmp = target.AddComponent<TextMeshProUGUI>();
            tmp.enabled = settings.enabled;
            tmp.fontStyle = settings.fontStyle;
            tmp.fontSize = settings.fontSize;
            tmp.fontSizeMin = settings.fontSizeMin;
            tmp.fontSizeMax = settings.fontSizeMax;
            tmp.lineSpacing = settings.lineSpacing;
            tmp.richText = settings.enableRichText;
            tmp.enableAutoSizing = settings.enableAutoSizing;
            tmp.alignment = settings.textAlignmentOptions;
            tmp.enableWordWrapping = settings.wrappingEnabled;
            tmp.overflowMode = settings.textOverflowModes;
            tmp.text = settings.text;
            tmp.color = settings.color;
            tmp.raycastTarget = settings.rayCastTarget;
        }
     
        static TextMeshProSettings GetTextMeshProSettings(GameObject gameObject)
        {
            Text uiText = gameObject.GetComponent<Text>();
            if (uiText == null)
            {
                EditorUtility.DisplayDialog("ERROR!", "You must select a Unity UI Text Object to convert.", "OK", "");
                return null;
            }
     
            return new TextMeshProSettings
            {
                enabled = uiText.enabled,
                fontStyle = FontStyleToFontStyles(uiText.fontStyle),
                fontSize = uiText.fontSize,
                fontSizeMin = uiText.resizeTextMinSize,
                fontSizeMax = uiText.resizeTextMaxSize,
                lineSpacing = uiText.lineSpacing,
                enableRichText = uiText.supportRichText,
                enableAutoSizing = uiText.resizeTextForBestFit,
                textAlignmentOptions = TextAnchorToTextAlignmentOptions(uiText.alignment),
                wrappingEnabled = HorizontalWrapModeToBool(uiText.horizontalOverflow),
                textOverflowModes = VerticalWrapModeToTextOverflowModes(uiText.verticalOverflow),
                text = uiText.text,
                color = uiText.color,
                rayCastTarget = uiText.raycastTarget
            };
        }
     
        static bool HorizontalWrapModeToBool(HorizontalWrapMode overflow)
        {
            return overflow == HorizontalWrapMode.Wrap;
        }
     
        static TextOverflowModes VerticalWrapModeToTextOverflowModes(VerticalWrapMode verticalOverflow)
        {
            return verticalOverflow == VerticalWrapMode.Truncate ? TextOverflowModes.Truncate : TextOverflowModes.Overflow;
        }
     
        static FontStyles FontStyleToFontStyles(FontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case FontStyle.Normal:
                    return FontStyles.Normal;
     
                case FontStyle.Bold:
                    return FontStyles.Bold;
     
                case FontStyle.Italic:
                    return  FontStyles.Italic;
     
                case FontStyle.BoldAndItalic:
                    return FontStyles.Bold | FontStyles.Italic;
            }
     
            Debug.LogWarning("Unhandled font style " + fontStyle);
            return FontStyles.Normal;
        }
     
        static TextAlignmentOptions TextAnchorToTextAlignmentOptions(TextAnchor textAnchor)
        {
            switch (textAnchor)
            {
                case TextAnchor.UpperLeft:
                    return TextAlignmentOptions.TopLeft;
     
                case TextAnchor.UpperCenter:
                    return TextAlignmentOptions.Top;
     
                case TextAnchor.UpperRight:
                    return TextAlignmentOptions.TopRight;
     
                case TextAnchor.MiddleLeft:
                    return TextAlignmentOptions.Left;
     
                case TextAnchor.MiddleCenter:
                    return TextAlignmentOptions.Center;
     
                case TextAnchor.MiddleRight:
                    return TextAlignmentOptions.Right;
     
                case TextAnchor.LowerLeft:
                    return TextAlignmentOptions.BottomLeft;
     
                case TextAnchor.LowerCenter:
                    return TextAlignmentOptions.Bottom;
     
                case TextAnchor.LowerRight:
                    return TextAlignmentOptions.BottomRight;
            }
     
            Debug.LogWarning("Unhandled text anchor " + textAnchor);
            return TextAlignmentOptions.TopLeft;
        }
    }
}