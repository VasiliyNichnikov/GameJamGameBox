using System;
using Core.Doors;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Monster
{
    public class MonsterPoint : MonoBehaviour, IComparable
    {
        [SerializeField] private RoomType Type;

        public Vector3 Position => transform.position;

        public float Weight => _weight;

        private float _weight;

        private IMonsterMovementCalculator _movementCalculator;
        private IMonsterEars _ears;
        private float _minBeta;
        private float _maxBeta;

        private const float NoiseRatio = 1f;

        public void Init(IMonsterMovementCalculator movementCalculator, IMonsterEars ears, float minBeta, float maxBeta)
        {
            _movementCalculator = movementCalculator;
            _ears = ears;

            _minBeta = minBeta;
            _maxBeta = maxBeta;
        }

        public bool TryUpdateWeight(float noiseDistance)
        {
            var beta = Random.Range(_minBeta, _maxBeta);

            var distanceFromPlayerToPoint = _movementCalculator.GetDistanceFromPlayerToPointRadius(Position);
            var distanceFromEnemyToPoint = _movementCalculator.GetDistanceFromEnemyToPointRadius(Position);

            // Если noiseRatio > 1, клетка услышала шум
            var noiseRatio = noiseDistance / distanceFromPlayerToPoint;
            if (noiseRatio >= NoiseRatio && distanceFromEnemyToPoint <= _ears.HearingDistance)
            {
                _weight = noiseRatio * beta;
                return true;
            }

            _weight = float.MinValue;
            return false;
        }

        public static int SortByWeight(MonsterPoint a, MonsterPoint b)
        {
            return a.CompareTo(b);
        }

        public int CompareTo(object obj)
        {
            if (obj is MonsterPoint point)
            {
                if (point._weight - _weight > 0.0f)
                {
                    return 1;
                }

                if (point._weight - _weight < 0.0f)
                {
                    return -1;
                }

                return 0;
            }

            Debug.LogError("Not corrected type obj");
            return 0;
        }
    }
}