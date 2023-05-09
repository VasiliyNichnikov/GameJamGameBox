#if UNITY_EDITOR
using System;
using System.Linq;
using Core.Monster;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MonsterEars))]
    public class MonsterEarsEditor : UnityEditor.Editor
    {
        private float _noiseVolume;
        private int _selectedPoint;
        private MonsterEars _monsterEars;

        private void OnEnable()
        {
            _monsterEars = (MonsterEars)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
            {
                return;
            }

            EditorGUILayout.LabelField("Is sound detected", $": {_monsterEars.GetSoundDetected()}");
            _noiseVolume = EditorGUILayout.FloatField("Noise volume", _noiseVolume);
            _selectedPoint = EditorGUILayout.IntSlider("Selected point", _selectedPoint, 0, _monsterEars.Points.Count - 1);

            if (GUILayout.Button("UpdatePoint"))
            {
                _monsterEars.SetCustomNoiseVolume(_selectedPoint, _noiseVolume);
            }

            if (GUILayout.Button("Update all points"))
            {
                _monsterEars.UpdateAllPointsNoiseVolume(_noiseVolume);
            }
        }

        private void OnSceneGUI()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            var point = _monsterEars.Points.ToArray()[_selectedPoint];
            Handles.PositionHandle(point.Position, Quaternion.identity);
            Handles.Label(point.Position, point.Weight.ToString());
        }
    }
}
#endif