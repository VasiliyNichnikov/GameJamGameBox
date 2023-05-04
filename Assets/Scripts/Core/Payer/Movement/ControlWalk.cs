using UnityEngine;

namespace Core.Payer.Movement
{
    [RequireComponent(typeof(PlayerWalk))]
    public class ControlWalk : MonoBehaviour
    {
        [SerializeField]
        private PlayerWalk _walk;
        
        private Vector3 _directionWalk;
        private Vector3 _cameraViewingAngle;

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
            
            Game.Instance.PlayerSound.PlayStep();
            _directionWalk = new Vector3(deltaX, 0, deltaZ);
            _walk.Walking(_directionWalk);
        }
        
        private bool CheckMovement(float deltaX, float deltaZ)
        {
            return deltaX != 0 || deltaZ != 0;
        }
    }
}