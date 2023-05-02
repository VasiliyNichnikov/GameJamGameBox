using UnityEngine;

namespace Core.Payer
{
    public class PlayerRotationCamera : MonoBehaviour
    {
        [SerializeField] private RotationAxes _axes;
    
        [SerializeField] private float _minimalVertical;
        [SerializeField] private float _maximumVertical;
    
        [Space] [SerializeField] private float _sensitivityX;
        [SerializeField] private PlayerAnimationCamera _animationCamera;
        private Transform _thisTransform;
        private float _rotationX, _rotationY;

        public RotationAxes Axes
        {
            get => _axes;
            set => _axes = value;
        }
        public float MinimalVertical
        {
            get => _minimalVertical;
            set => _minimalVertical = value;
        }
        public float MaximumVertical
        {
            get => _maximumVertical;
            set => _maximumVertical = value;
        }
        public float SensitivityX
        {
            get => _sensitivityX;
            set => _sensitivityX = value;
        }
        public PlayerAnimationCamera AnimationCamera
        {
            get => _animationCamera;
            set => _animationCamera = value;
        }

        private void Start()
        {
            _thisTransform = transform;
        }

        private void Update()
        {
            // todo нужно использовать свой обработчик
            if (_axes == RotationAxes.MouseX)
            {
                MouseX();
            }
            else if (_axes == RotationAxes.MouseY)
            {
                MouseY();
            }
        }
    
        private void MouseX()
        {
            _thisTransform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivityX, 0);
        }

        private void MouseY()
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensitivityX;
            _rotationX = Mathf.Clamp(_rotationX, _minimalVertical, _maximumVertical);
        
            _rotationY = _thisTransform.localEulerAngles.y;
        
            float rotationZ = _animationCamera.GetRotationZ();

            _thisTransform.localEulerAngles =  new Vector3(_rotationX, _rotationY, rotationZ);
        }
    
    
    }
}