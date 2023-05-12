using UnityEngine;

namespace Core.Payer.Movement
{
    [RequireComponent(typeof(PlayerWalk))]
    public class ControlWalk : MonoBehaviour
    {
        [SerializeField] private PlayerWalk _walk;
        [SerializeField] private float _noiseyVolume;

        private Vector3 _directionWalk;
        private Vector3 _cameraViewingAngle;

        private const KeyCode RightShift = KeyCode.RightShift;
        private const KeyCode LeftShift = KeyCode.LeftShift;
        
        public static bool IsMovement { get; private set; }

        private void Start()
        {
            Main.Instance.InputHandler.OnAnyKey += Move;
        }

        private void OnDestroy()
        {
            Main.Instance.InputHandler.OnAnyKey -= Move;
        }

        private void Move()
        {
            float deltaX = Input.GetAxis("Horizontal");
            float deltaZ = Input.GetAxis("Vertical");

            IsMovement = CheckMovement(deltaX, deltaZ);

            if (!IsMovement)
            {
                return;
            }

            if (Input.GetKey(RightShift) || Input.GetKey(LeftShift))
            {
                _walk.SetRunSpeed();
            }
            else
            {
                _walk.SetWalkSpeed();
            }
            
            _directionWalk = new Vector3(deltaX, 0, deltaZ);
            _walk.Walking(_directionWalk);
            Game.Instance.PlayerSound.MakeSound(_noiseyVolume);
        }

        private bool CheckMovement(float deltaX, float deltaZ)
        {
            return deltaX != 0 || deltaZ != 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _noiseyVolume);
        }
#endif
    }
}