using System;
using System.Collections.Generic;
using System.Linq;
using Core.Payer;
using UnityEngine;

namespace Core.Monster
{
    /// <summary>
    /// Уши монстра
    /// </summary>
    public class MonsterEars : MonoBehaviour, IMonsterEars, IDisposable
    {
        public Vector3 LoudestPoint => _loudestPoint ?? Vector3.zero;

        public IReadOnlyCollection<MonsterPoint> Points => _points;
        public float HearingDistance => _hearingDistance;
        
        private List<MonsterPoint> _points = new List<MonsterPoint>();

        [SerializeField] private Transform _bodyMonster;

        [SerializeField, Header("Дистанция слуха монстра")]
        private float _hearingDistance;

        [SerializeField] private Color _color = new Color(1, 0, 0.25f);
        
        private Vector3? _loudestPoint;

        public void Init(IEnumerable<MonsterPoint> points)
        {
            _points = points.ToList();
            PlayerNoiseCatcher.OnMakingSound += CatchPlayerNoise;
        }

        public bool SoundDetected()
        {
            return _loudestPoint != null;
        }
        
        /// <summary>
        /// Ловим шум игрока
        /// </summary>
        private void CatchPlayerNoise(float noiseDistance)
        {
            var soundDetected = false;
            foreach (var point in _points)
            {
                soundDetected |= point.TryUpdateWeight(noiseDistance);
            }

            if (soundDetected)
            {
                _points.Sort(MonsterPoint.SortByWeight);
                _loudestPoint = _points[0].Position;
            }
            else
            {
                _loudestPoint = null;
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