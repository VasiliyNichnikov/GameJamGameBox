using System;
using System.Collections.Generic;
using System.Linq;
using Core.Monster.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Monster
{
    public class MonsterBrain : MonoBehaviour
    {
        [SerializeField] private MonsterWalking _legs;
        [SerializeField] private MonsterEars _ears;
        [SerializeField] private MonsterEyes _eyes;

        [SerializeField] private Transform _parentPoints;
        [SerializeField] private SettingsWalkingLogic _settings;

        private List<Vector3> _pointsForMovement;
        private List<MonsterPoint> _monsterPoints;
        private MonsterStateMachine _stateMachine;
        private Transform _playerTransform;

        private void Awake()
        {
            _pointsForMovement = new List<Vector3>();
            var children = _parentPoints.GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();

            foreach (var pointTransform in children)
            {
                var position = pointTransform.position;
                _pointsForMovement.Add(new Vector3(position.x, 0.0f, position.z));
            }
#if !UNITY_EDITOR
            foreach (var kid in children)
            {
                Destroy(kid.gameObject);
            }
#endif
            Debug.LogWarning($"Points for movement: {_pointsForMovement.Count}");
        }

        /// <summary>
        /// Внимание:
        /// Срабатывает в Awake Game, который происходит раньше Awake в MonsterBrain
        /// </summary>
        /// <param name="player"></param>
        public void Init(Transform player)
        {
            _playerTransform = player;
            
            _stateMachine = new MonsterStateMachine();
            _stateMachine.InitWalkState(_legs, _ears, _eyes).InitRunState(_legs, _eyes).SetInitialized();
            _stateMachine.InitStartingState(_stateMachine.WalkingState);
            
            InitPoints();
            _legs.Init(_playerTransform, _pointsForMovement);
            _ears.Init(_monsterPoints);
        }

        public void Update()
        {
            if (!_stateMachine.IsInitialized)
            {
                Debug.LogWarning("Monster's state machine not initialized.");
                return;
            }

            _stateMachine.CurrentState.LogicUpdate();
            _legs.Move();
        }

        private void InitPoints()
        {
            _monsterPoints = new List<MonsterPoint>();
            foreach (var point in _pointsForMovement)
            {
                var readyPoint = new MonsterPoint(_legs,
                    _ears,
                    point,
                    _settings.MinBeta,
                    _settings.MaxBeta);
                _monsterPoints.Add(readyPoint);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_parentPoints == null || Application.isPlaying)
            {
                return;
            }
            
            var children = _parentPoints.GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();

            Gizmos.color = Color.cyan;
            foreach (var pointTransform in children)
            {
                var position = pointTransform.position;
                Gizmos.DrawSphere(position, 0.3f);
            }
        }
#endif
    }
}