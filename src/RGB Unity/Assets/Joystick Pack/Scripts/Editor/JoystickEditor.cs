﻿using Joystick_Pack.Scripts.Base;
using UnityEditor;
using UnityEngine;

namespace Joystick_Pack.Scripts.Editor
{
    [CustomEditor(typeof(Joystick), true)]
    public class JoystickEditor : UnityEditor.Editor
    {
        private SerializedProperty _handleRange;
        private SerializedProperty _deadZone;
        private SerializedProperty _axisOptions;
        private SerializedProperty _snapX;
        private SerializedProperty _snapY;
        protected SerializedProperty background;
        private SerializedProperty _handle;

        protected Vector2 center = new Vector2(0.5f, 0.5f);

        protected virtual void OnEnable()
        {
            _handleRange = serializedObject.FindProperty("handleRange");
            _deadZone = serializedObject.FindProperty("deadZone");
            _axisOptions = serializedObject.FindProperty("axisOptions");
            _snapX = serializedObject.FindProperty("snapX");
            _snapY = serializedObject.FindProperty("snapY");
            background = serializedObject.FindProperty("background");
            _handle = serializedObject.FindProperty("handle");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawValues();
            EditorGUILayout.Space();
            DrawComponents();

            serializedObject.ApplyModifiedProperties();

            if(_handle != null)
            {
                RectTransform handleRect = (RectTransform)_handle.objectReferenceValue;
                handleRect.anchorMax = center;
                handleRect.anchorMin = center;
                handleRect.pivot = center;
                handleRect.anchoredPosition = Vector2.zero;
            }
        }

        protected virtual void DrawValues()
        {
            EditorGUILayout.PropertyField(_handleRange, new GUIContent("Handle Range", "The distance the visual handle can move from the center of the joystick."));
            EditorGUILayout.PropertyField(_deadZone, new GUIContent("Dead Zone", "The distance away from the center input has to be before registering."));
            EditorGUILayout.PropertyField(_axisOptions, new GUIContent("Axis Options", "Which axes the joystick uses."));
            EditorGUILayout.PropertyField(_snapX, new GUIContent("Snap X", "Snap the horizontal input to a whole value."));
            EditorGUILayout.PropertyField(_snapY, new GUIContent("Snap Y", "Snap the vertical input to a whole value."));
        }

        protected virtual void DrawComponents()
        {
            EditorGUILayout.ObjectField(background, new GUIContent("Background", "The background's RectTransform component."));
            EditorGUILayout.ObjectField(_handle, new GUIContent("Handle", "The handle's RectTransform component."));
        }
    }
}