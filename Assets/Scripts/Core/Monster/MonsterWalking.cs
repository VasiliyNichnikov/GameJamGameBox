using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Core.Monster
{
    public class MonsterWalking : MonoBehaviour, IMonsterWalking, IMonsterMovementCalculator
    {
        [Serializable]
        private struct SpeedSettings
        {
            public float Speed => _speed;
            public SpeedType Type => _type;

            [SerializeField] private float _speed;
            [SerializeField] private SpeedType _type;
        }

        #region DebugMode

#if UNITY_EDITOR
        public bool IsDebugMode => _isDebugMode;

        [SerializeField] private bool _isDebugMode;
        [HideInInspector] public bool GoToNextPoint;
#endif

        #endregion

        [SerializeField] private Transform _bodyMonster;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _minDistanceToPoint;
        [SerializeField] private float _minDistanceToPlayer;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpeedSettings[] _speedSettings;

        private IList<Vector3> _pointsForMovement;
        private Transform _playerTransform;

        private Vector3 _currentPointOfMovement;
        
        private NavMeshPath _path;
        private Vector3 _destination;
        private Vector3 _agentPosition;
        private int _pathIteration = 1;

        private const float OffsetRadius = 0.1f;
        private static readonly int Speed = Animator.StringToHash("Speed");

        public bool IsMovement => !_agent.isStopped && _currentPointOfMovement != Vector3.zero;
        public bool IsPlayerNearby => _agent.remainingDistance <= _minDistanceToPlayer;

        private void Awake()
        {
#if UNITY_EDITOR
            if (_isDebugMode)
            {
                GoToNextPoint = false;
            }
#endif
            _path = new NavMeshPath();
            _currentPointOfMovement = Vector3.zero;
            _agent.stoppingDistance = _minDistanceToPoint;
        }

        public void Init(Transform playerTransform, IList<Vector3> pointsForMovement)
        {
            _playerTransform = playerTransform;
            _pointsForMovement = pointsForMovement;
        }

        public void SetSpeedMovement(SpeedType speedType)
        {
            var speed = GetSpeedByType(speedType);
            _animator.SetFloat(Speed, speed);
            _agent.speed = speed;
            _agent.acceleration = speed;
        }

        public void SearchPlayer()
        {
            if (IsMovement)
            {
                return;
            }
            
            _currentPointOfMovement = GetRandomPointForMovement();
            UpdatePath();
        }

        public void GoForSound(Vector3 loudestPoint)
        {
            if (_isDebugMode)
            {
                return;
            }

            Debug.LogWarning("GoForSound");
            _currentPointOfMovement = loudestPoint;
            UpdatePath();
        }

        public void RunAfterPlayer()
        {
            if (_isDebugMode)
            {
                return;
            }

            var playerPosition = _playerTransform.position;
            _currentPointOfMovement = playerPosition;
            UpdatePath();
        }

        public void Move()
        {
            SetAgentPosition();
            if (_path.corners.Length == 0)
            {
                return;
            }

            if (_pathIteration >= _path.corners.Length)
            {
                _pathIteration = 1;
                _agent.isStopped = true;
                _destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                return;
            }
            _destination = _path.corners[_pathIteration];

            if (_destination.x < float.PositiveInfinity)
            {
                var speedRotation = GetSpeedByType(SpeedType.Rotation);
                var direction = _destination - _agentPosition;
                
                var newDirection = Vector3.RotateTowards(_bodyMonster.forward, direction, speedRotation * Time.deltaTime, 0.0f);
                var newRotation = Quaternion.LookRotation(newDirection);
                _bodyMonster.rotation = Quaternion.Slerp(_bodyMonster.rotation, newRotation, _agent.speed * Time.deltaTime * 2.0f);

                var distance = Vector3.Distance(_agentPosition, _destination);
                if (distance > _agent.radius + OffsetRadius)
                {
                    var movement = _bodyMonster.forward * Time.deltaTime * _agent.speed;
                    _agent.isStopped = false;
                    _agent.Move(movement);
                }
                else
                {
                    ++_pathIteration;
                    if (_pathIteration >= _path.corners.Length)
                    {
                        _destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity,
                            float.PositiveInfinity);
                        _pathIteration = 1;
                        _agent.isStopped = true;
                    }
                }
            }
        }

        private void SetAgentPosition()
        {
            if (NavMesh.SamplePosition(_bodyMonster.position, out var hit, 1.0f, NavMesh.AllAreas))
            {
                _agentPosition = hit.position;
            }
        }
        
        private void UpdatePath()
        {
            _agent.CalculatePath(_currentPointOfMovement, _path);
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

        private float GetSpeedByType(SpeedType type)
        {
            return _speedSettings.FirstOrDefault(settings => settings.Type == type).Speed;
        }

        /*private Vector3[] GetNearestPointsForMovement()
        {
            var result = new List<Vector3>();
            foreach (var corner in _path.corners)
            {
                var minDistance = Mathf.Infinity;
                Vector3? currentPoint = null;
                foreach (var point in _pointsForMovement)
                {
                    var distance = Vector3.Distance(point, corner); 
                    if (distance < minDistance)
                    {
                        currentPoint = point;
                        minDistance = distance;
                    }
                }

                if (currentPoint != null && 
                    Vector3.Distance(currentPoint.Value, corner) <= 1f &&
                    !result.Contains(currentPoint.Value))
                {
                    Debug.LogWarning($"Current point: {currentPoint.Value}");
                    result.Add(currentPoint.Value);
                }
                else
                {
                    result.Add(corner);
                }
            }

            return result.ToArray();
        }*/
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_pointsForMovement != null && _pointsForMovement.Count != 0)
            {
                DrawPoint(_pointsForMovement);
            }
            
            Gizmos.color = Color.yellow;
            if(_path != null && _path.corners.Length > 0)
            {
                var previewPosition = _agentPosition;
                for(int i = _pathIteration; 
                    i < _path.corners.Length; ++i)
                {
                    Gizmos.DrawLine(previewPosition, _path.corners[i]);
                    previewPosition = _path.corners[i];
                }
            }
        }

        private void DrawPoint(IEnumerable<Vector3> points)
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