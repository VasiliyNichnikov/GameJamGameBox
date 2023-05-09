using System;
using System.Collections.Generic;
using Core.Payer;
using UnityEngine;

namespace Core.Monster
{
    /// <summary>
    /// Уши монстра
    /// </summary>
    public class MonsterEars : MonoBehaviour, IMonsterEars, IDisposable
    {
        public Vector3 LoudestPoint => _loudestPoint;
        public IReadOnlyCollection<MonsterPoint> Points => _points;
        public float HearingDistance => _hearingDistance;
        public bool GetSoundDetected() => _soundDetected;
        private List<MonsterPoint> _points = new List<MonsterPoint>();

        [SerializeField] private Transform _bodyMonster;

        [SerializeField, Header("Дистанция слуха монстра")]
        private float _hearingDistance;

        [SerializeField] private Color _color = new Color(1, 0, 0.25f);

        private bool _soundDetected;
        
        private Vector3 _loudestPoint;

        public void Init(List<MonsterPoint> points)
        {
            _points = points;
            PlayerNoiseCatcher.OnMakingSound += CatchPlayerNoise;
        }

        public bool SoundDetected()
        {
            var temp = _soundDetected;
            _soundDetected = false;
            return temp;
        }
        
        /// <summary>
        /// Ловим шум игрока
        /// </summary>
        private void CatchPlayerNoise(float noiseDistance)
        {
            foreach (var point in _points)
            {
                _soundDetected |= point.TryUpdateWeight(noiseDistance);
            }

            if (_soundDetected)
            {
                _points.Sort(MonsterPoint.SortByWeight);
                _loudestPoint = _points[0].Position;
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            PlayerNoiseCatcher.OnMakingSound -= CatchPlayerNoise;
        }


#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (_bodyMonster == null)
            {
                return;
            }

            Gizmos.color = _color;
            Gizmos.DrawSphere(_bodyMonster.position, _hearingDistance);
        }

        /// <summary>
        /// UNITY EDITOR
        /// </summary>
        public void SetCustomNoiseVolume(int numberPoint, float weight)
        {
            if (numberPoint >= 0 && numberPoint < _points.Count)
            {
                _points[numberPoint].TryUpdateWeight(weight);
            }
        }

        /// <summary>
        /// UNITY EDITOR
        /// </summary>
        public void UpdateAllPointsNoiseVolume(float noiseVolume)
        {
            Game.Instance.PlayerSound.MakeSound(noiseVolume);
        }
#endif
    }
}