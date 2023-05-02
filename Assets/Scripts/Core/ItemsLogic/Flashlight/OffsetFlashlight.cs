using UnityEngine;

namespace Core.ItemsLogic.Flashlight
{
    public class OffsetFlashlight : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _speed;
    
        private Transform _thisTransform;
        private Vector3 _vectorOffset;

        private void Start()
        {
            _thisTransform = transform;
            _vectorOffset = _thisTransform.position - _camera.position;
        }

        private void Update()
        {
            _thisTransform.position = _camera.position + _vectorOffset;
            _thisTransform.rotation =
                Quaternion.Slerp(_thisTransform.rotation, _camera.rotation, _speed * Time.deltaTime);
        }
    }
}