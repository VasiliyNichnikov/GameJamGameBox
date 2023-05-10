#if UNITY_EDITOR
using Core.Monster;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MonsterEyes))]
    [CanEditMultipleObjects]
    public class MonsterEyesEditor : UnityEditor.Editor
    {
        private MonsterEyes _eyes;

        private void OnEnable()
        {
            _eyes = (MonsterEyes)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
            {
                return;
            }
            
            var value = _eyes.IsPlayerVisible ? "Yes" : "No";
            EditorGUILayout.LabelField($"IsVisiblePlayer", value);
        }
    }
}
#endif