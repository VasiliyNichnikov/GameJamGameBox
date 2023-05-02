using System;
using UnityEngine;

namespace Core.InputSystem
{
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        public event Action<KeyCode> OnInputKeyboard;
        public event Action OnInputMouseButton;

        private const KeyCode KeyE = KeyCode.K;
        private const KeyCode KeySpace = KeyCode.Space;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                OnInputMouseButton?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyE))
            {
                OnInputKeyboard?.Invoke(KeyE);
            }

            if (Input.GetKeyDown(KeySpace))
            {
                OnInputKeyboard?.Invoke(KeySpace);
            }
        }
    }
}