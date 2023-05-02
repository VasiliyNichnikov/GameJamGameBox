using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _gravity = -9.8f;

    Vector3 _velosity;

    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        var currentSpeed = GetCurrentSpeed();
        _characterController.Move(move * currentSpeed * Time.deltaTime);
        _velosity.y = _gravity;
        _characterController.Move(_velosity * Time.deltaTime);
    }

    private float GetCurrentSpeed()
    {
        return Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _moveSpeed;
    }
}
