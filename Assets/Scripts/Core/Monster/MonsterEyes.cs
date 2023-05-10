using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core.Monster
{
    public class MonsterEyes : MonoBehaviour, IMonsterEyes
    {
        [SerializeField] private Transform _monsterBody;
        [SerializeField] private Transform _visibilityAreaDirection;
        
        [SerializeField, Range(-180, 180)] private float _fow;
        [SerializeField, Range(1, 1000)] private float _rayCount;
        [SerializeField, Range(0, 100)] private float _viewDistance;
        [SerializeField, Range(0, 360)] private float _shiftY;
        [Space] [SerializeField] private LayerMask _playerMask;
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private int _colliderMax;

        public bool IsPlayerVisible { get; private set; }

        private void Start()
        {
            StartCoroutine(FindTargetsWithDelay(0.1f));
        }

        private IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisiblePlayer();
            }
        }

        private void FindVisiblePlayer()
        {
            var hitColliders = new Collider[_colliderMax];
            var numberColliders =
                Physics.OverlapSphereNonAlloc(_monsterBody.position, _viewDistance, hitColliders, _playerMask);

            IsPlayerVisible = false;
            for (var i = 0; i < numberColliders; i++)
            {
                var target = hitColliders[i];
                var directoryToTarget = target.transform.position - _monsterBody.position;

                var from = _visibilityAreaDirection.position - _monsterBody.position;
                if (!(Vector3.Angle(from, directoryToTarget) < _fow / 2))
                {
                    continue;
                }

                var distanceToTarget = Vector3.Distance(_monsterBody.position, target.transform.position);
                IsPlayerVisible |= !Physics.Raycast(_monsterBody.position,
                    directoryToTarget,
                    distanceToTarget,
                    _obstacleMask);
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_monsterBody == null)
            {
                return;
            }

            const float positionY = 0.5f;
            var origin = _monsterBody.position;
            var newOrigin = new Vector3(origin.x, positionY, origin.z);


            Gizmos.color = new Color(0f, 0f, 1f, 0.3f);
            var angle = -_monsterBody.rotation.eulerAngles.y + _shiftY;

            var angleIncrease = _fow / _rayCount;

            for (var i = 0; i <= _rayCount; i++)
            {
                var direction = -Vector3Utils.GetVectorFromAngle(angle) * _viewDistance;

                var newDirection = new Vector3(direction.x, positionY, direction.z);
                Gizmos.DrawRay(newOrigin, newDirection);
                angle -= angleIncrease;
            }
        }
#endif
    }
}