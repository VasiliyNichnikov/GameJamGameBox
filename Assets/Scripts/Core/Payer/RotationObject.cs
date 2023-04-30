using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] private float _xRotationAngel;
    [SerializeField] private float _yRotationAngel;
    [SerializeField] private float _zRotationAngel;
    private bool _result1;
    private bool _result2;
    private bool _result3;



    private enum _typeObject
    {
        None, object_1, object_2, object_3
    }

    [SerializeField] private _typeObject _typeOB;


    void Update()
    {
        switch(_typeOB.ToString())
        {
            case "object_1":
                if (Input.GetKeyDown(KeyCode.R))
                {
                    transform.rotation = Quaternion.Euler(_xRotationAngel, _yRotationAngel, _zRotationAngel);
                    Debug.Log(_xRotationAngel);
                    Debug.Log(transform.localEulerAngles.x);
                    Debug.Log(_yRotationAngel);
                    Debug.Log(transform.localEulerAngles.y);
                    Debug.Log(_zRotationAngel);
                    Debug.Log(transform.localEulerAngles.z);
                }
                if (transform.localEulerAngles.x == _xRotationAngel && transform.localEulerAngles.y == _yRotationAngel && transform.localEulerAngles.z == _zRotationAngel)
                {
                    _result1 = true;
                }

                break;

            case "object_2":
                if (Input.GetKeyDown(KeyCode.T))
                {
                    transform.rotation = Quaternion.Euler(_xRotationAngel, _yRotationAngel, _zRotationAngel);
                    Debug.Log(_xRotationAngel);
                    Debug.Log(transform.localEulerAngles.x);
                    Debug.Log(_yRotationAngel);
                    Debug.Log(transform.localEulerAngles.y);
                    Debug.Log(_zRotationAngel);
                    Debug.Log(transform.localEulerAngles.z);
                }
                if (transform.localEulerAngles.x == _xRotationAngel && transform.localEulerAngles.y == _yRotationAngel && transform.localEulerAngles.z == _zRotationAngel)
                {
                    _result2 = true;
                    
                }
                break;

            case "object_3":
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    transform.rotation = Quaternion.Euler(_xRotationAngel, _yRotationAngel, _zRotationAngel);
                    Debug.Log(_xRotationAngel);
                    Debug.Log(transform.localEulerAngles.x);
                    Debug.Log(_yRotationAngel);
                    Debug.Log(transform.localEulerAngles.y);
                    Debug.Log(_zRotationAngel);
                    Debug.Log(transform.localEulerAngles.z);
                }
                if (transform.localEulerAngles.x == _xRotationAngel && transform.localEulerAngles.y == _yRotationAngel && transform.localEulerAngles.z == _zRotationAngel)
                {
                    _result3 = true;
                    
                }
            break;

            default:
                _typeOB = _typeObject.None;
            break;
        }
        if (_result1 == true && _result2 == true && _result3 == true)
        {
            print("ЗАРАБОТАЛО");
        }
    }
}

//_xRotationAngel == 45 && _yRotationAngel == 0 && _zRotationAngel == 90