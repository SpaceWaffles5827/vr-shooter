﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FishNet.Managing.Client.Editing
{


    [CustomEditor(typeof(ClientManager), true)]
    [CanEditMultipleObjects]
    public class ClientManagerEditor : Editor
    {
        private SerializedProperty _remoteServerTimeout;
        private SerializedProperty _changeFrameRate;
        private SerializedProperty _frameRate;

        protected virtual void OnEnable()
        {
            _remoteServerTimeout = serializedObject.FindProperty(nameof(_remoteServerTimeout));
            _changeFrameRate = serializedObject.FindProperty(nameof(_changeFrameRate));
            _frameRate = serializedObject.FindProperty(nameof(_frameRate));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((ClientManager)target), typeof(ClientManager), false);
            GUI.enabled = true;

            //EditorGUILayout.PropertyField(_remoteServerTimeout);
            EditorGUILayout.PropertyField(_changeFrameRate);
            if (_changeFrameRate.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_frameRate);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }

    }
}
#endif