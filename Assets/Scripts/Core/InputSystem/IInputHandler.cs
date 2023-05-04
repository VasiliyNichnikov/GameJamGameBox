using System;
using UnityEngine;

namespace Core.InputSystem
{
    public interface IInputHandler
    {
        event Action<KeyCode> OnInputKeyboard;
        event Action OnAnyKey;
        event Action OnInputMouseButton;
    }
}