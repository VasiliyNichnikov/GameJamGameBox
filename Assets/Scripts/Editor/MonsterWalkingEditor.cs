using Core.Monster;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MonsterWalking))]
    public class MonsterWalkingEditor : UnityEditor.Editor
    {
        private MonsterWalking _walking;

        private void OnEnable()
        {
            _walking = (MonsterWalking)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Go to next point") && _walking.IsDebugMode)
            {
                _walking.GoToNextPoint = true;
            }
        }
    }
}