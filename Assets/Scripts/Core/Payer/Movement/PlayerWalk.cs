using Core.SoundLogic;
using UnityEngine;

namespace Core.Payer.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerWalk : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        private CharacterController _character;

        private float _currentSpeed;
        private PlayerSound.StepType _stepType;
        
        private void Start()
        {
            _character = GetComponent<CharacterController>();
            SetWalkSpeed();
        }

        public void SetWalkSpeed()
        {
            _currentSpeed = _walkSpeed;
            _stepType = PlayerSound.StepType.Walk;
        }

        public void SetRunSpeed()
        {
            _currentSpeed = _runSpeed;
            _stepType = PlayerSound.StepType.Run;
        }
        
        /// <summary>
        /// Передвигает объект по заданному направлению
        /// </summary>
        /// <param name="movement"></param>
        public void Walking(Vector3 movement)
        {
            movement = Vector3.ClampMagnitude(movement, _currentSpeed);
            movement = transform.TransformDirection(movement);
            _character.Move(movement * _currentSpeed * Time.deltaTime);
            Game.Instance.PlayerSound.PlayStep(_stepType);
            transform.localPosition = new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z);
        }

    }
}
