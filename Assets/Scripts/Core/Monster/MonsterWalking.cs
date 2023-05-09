using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Core.Monster
{
    public class MonsterWalking : MonoBehaviour, IMonsterWalking, IMonsterMovementCalculator
    {
        #region DebugMode
#if UNITY_EDITOR
        public bool IsDebugMode => _isDebugMode;
        
        [SerializeField] private bool _isDebugMode;
        [HideInInspector] public bool GoToNextPoint;
#endif
        #endregion

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _minDistanceWithPoint;
        [SerializeField] private float _minDistanceToPlayer;


        private IReadOnlyList<Vector3> _pointsForMovement;
        private Transform _playerTransform;
        private IMonsterEars _ears;

        private Vector3 _currentPointOfMovement;
        
        public bool IsMovement => _agent.remainingDistance > _minDistanceWithPoint;

        public bool IsPlayerVisible
        {
            get
            {
                var path = new NavMeshPath();
                _agent.CalculatePath(_playerTransform.position, path);
                return GetDistance(path) < _minDistanceToPlayer;
            }
        }

        private void Awake()
        {
#if UNITY_EDITOR
            if (_isDebugMode)
            {
                GoToNextPoint = false;
            }
#endif
            _agent.stoppingDistance = _minDistanceWithPoint;
        }

        public void Init(Transform playerTransform, IMonsterEars ears, IReadOnlyList<Vector3> pointsForMovement)
        {
            _ears = ears;
            _playerTransform = playerTransform;
            _pointsForMovement = pointsForMovement;
        }
        
        public void SearchPlayer()
        {
            var canGoToNextPoint = IsCanToGo();
            if (!canGoToNextPoint)
            {
                return;
            }

            _currentPointOfMovement = GetRandomPointForMovement();
            UpdateSetDestination();
        }

        public void GoForSound()
        {
            var canGoToNextPoint = IsCanToGo();
            if (!canGoToNextPoint)
            {
                return;
            }
            
            _currentPointOfMovement = _ears.LoudestPoint;
            UpdateSetDestination();
        }

        public void RunAfterPlayer()
        {
            _currentPointOfMovement = _playerTransform.position;
            UpdateSetDestination();
        }

        private bool IsCanToGo()
        {
#if UNITY_EDITOR
            if (_isDebugMode && !GoToNextPoint)
            {
                return false;
            }

            GoToNextPoint = false;
#endif
            
            if (IsMovement)
            {
                return false;
            }

            return true;
        }

        private void UpdateSetDestination()
        {
            _agent.SetDestination(_currentPointOfMovement);
        }

        private Vector3 GetRandomPointForMovement()
        {
            var randomValue = Random.Range(0, _pointsForMovement.Count);
            return _pointsForMovement[randomValue];
        }

        public float GetDistanceFromPlayerToPointRadius(Vector3 point)
        {
            return Vector3.Distance(_playerTransform.position, point);
        }

        public float GetDistanceFromEnemyToPointRadius(Vector3 point)
        {
            return Vector3.Distance(transform.position, point);
        }

        private float GetDistance(NavMeshPath path)
        {
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                return float.MaxValue;
            }

            var result = .0f;
            for (var i = 0; i < path.corners.Length - 1; i++)
            {
                var currentPoint = path.corners[i];
                var nextPoint = path.corners[i + 1];

                var distance = Vector3.Distance(currentPoint, nextPoint);
                result += distance;
            }

            return result;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_pointsForMovement != null && _pointsForMovement.Count != 0)
            {
                DrawPoint(_pointsForMovement);
            }
        }

        private void DrawPoint(IReadOnlyList<Vector3> points)
        {
            foreach (var point in points)
            {
                Gizmos.color = _currentPointOfMovement == point ? Color.blue : Color.magenta;
                Gizmos.DrawSphere(point, 0.15f);
            }
        }
#endif
    }
}