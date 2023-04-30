using UnityEngine;

namespace Core.Payer
{
    public class BodyOfRotation : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            var playerPlane = new Plane(Vector3.up, transform.position);
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (playerPlane.Raycast(ray, out var hitDistance))
            {
                Vector3 targetPoint = ray.GetPoint(hitDistance);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _moveSpeed * Time.deltaTime);
            }
        }
    }
}