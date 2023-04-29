using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    private float _corentSpeed;
    private float _x_Move;
    private float _z_Move;
    private CharacterController _player;
    private Vector3 _move_Derection;

    void Start()
    {
        _player = GetComponent<CharacterController>();    
    }

    void Update()
    {
        Move();    
    }

    void Move()
    {
        _x_Move = Input.GetAxis("Horizontal");
        _z_Move = Input.GetAxis("Vertical");
        if (_player.isGrounded)
        {
            _move_Derection = new Vector3(_x_Move, 0f,_z_Move);
            _move_Derection = transform.TransformDirection(_move_Derection);
        }

        _move_Derection.y = -1;

        if ( Input.GetKey(KeyCode.LeftShift))
        {
            _corentSpeed = MoveSpeed * 2;
        }
        else
        {
            _corentSpeed = MoveSpeed;
        }
        _player.Move(_move_Derection * _corentSpeed * Time.deltaTime);
    }
}
