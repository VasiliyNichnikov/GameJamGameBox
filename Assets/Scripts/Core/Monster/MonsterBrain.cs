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
        [SerializeField] private MonsterPoint[] _monsterPoints;
        
        private MonsterStateMachine _stateMachine;
        private Transform _playerTransform;

        private bool _isInitialized;

        /// <summary>
        /// Внимание:
        /// Срабатывает в Awake Game, который происходит раньше Awake в MonsterBrain
        /// </summary>
        /// <param name="player"></param>
        public void Init(Transform player)
        {
            if (_isInitialized)
            {
                return;
            }
            
            _playerTransform = player;
            
            _stateMachine = new MonsterStateMachine();
            _stateMachine.InitWalkState(_legs, _ears, _eyes).InitRunState(_legs, _eyes).SetInitialized();
            _stateMachine.InitStartingState(_stateMachine.WalkingState);

            InitPoints();
            _legs.Init(_playerTransform, _monsterPoints.Select(p => p.Position).AsReadOnlyList());
            _ears.Init(_monsterPoints);

            _isInitialized = true;
        }

        public void Update()
        {
            if (!_isInitialized)
            {
                return;
            }
            
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
            foreach (var point in _monsterPoints)
            {
                point.Init(_legs, _ears, _settings.MinBeta, _settings.MaxBeta);
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