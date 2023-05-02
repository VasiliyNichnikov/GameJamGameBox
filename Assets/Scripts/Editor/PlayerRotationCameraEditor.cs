using Core.Payer;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PlayerRotationCamera))]
    public class PlayerRotationCameraEditor : UnityEditor.Editor
    {
        private PlayerRotationCamera _rotationCamera;
        private SerializedObject _serialized;

        private void OnEnable()
        {
            _rotationCamera = (PlayerRotationCamera) target;
            _serialized = new SerializedObject(_rotationCamera);
        }

        public override void OnInspectorGUI()
        {
            _rotationCamera.Axes = (RotationAxes) EditorGUILayout.EnumPopup("Тип вращения:", _rotationCamera.Axes);
            if (_rotationCamera.Axes == RotationAxes.MouseY)
            {
                EditorGUILayout.BeginVertical("box");
                _rotationCamera.MinimalVertical =
                    EditorGUILayout.Slider("Minimal vertical:", _rotationCamera.MinimalVertical, -90f, 90f);
                _rotationCamera.MaximumVertical =
                    EditorGUILayout.Slider("Maximum vertical:", _rotationCamera.MaximumVertical, -90f, 90f);
                _rotationCamera.AnimationCamera =
                    (PlayerAnimationCamera) EditorGUILayout.ObjectField("Animation Camera:",
                        _rotationCamera.AnimationCamera,
                        typeof(PlayerAnimationCamera), true);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            _rotationCamera.SensitivityX = EditorGUILayout.Slider("Sensitivity X:", _rotationCamera.SensitivityX, 0f, 100f);
            CheckChanges();
        }

        private void CheckChanges()
        {
            if (GUI.changed)
            {
                EditorUtility.SetDirty(_rotationCamera);
                _serialized.ApplyModifiedProperties();
            }
        }
    }
}