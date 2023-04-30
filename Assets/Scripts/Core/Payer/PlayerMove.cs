using UnityEngine;

namespace Core.Payer
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _runSpeed;

        private float _xMove;
        private float _zMove;
        private CharacterController _player;
        private Vector3 _moveDirection;

        private void Start()
        {
            _player = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _xMove = Input.GetAxis("Horizontal");
            _zMove = Input.GetAxis("Vertical");
            if (_player.isGrounded)
            {
                _moveDirection = new Vector3(_xMove, 0f, _zMove);
                _moveDirection = transform.TransformDirection(_moveDirection);
            }

            _moveDirection.y = -1;

            var currentSpeed = GetCurrentSpeed();
            _player.Move(_moveDirection * currentSpeed * Time.deltaTime);
        }

        private float GetCurrentSpeed()
        {
            return Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _moveSpeed;
        }
    }
}