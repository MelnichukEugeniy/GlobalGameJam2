using UnityEngine;

namespace Player.Input
{
    [CreateAssetMenu(fileName = nameof(PlayerMovementInput), menuName = "Player/MovementInput")]
    public class PlayerMovementInput : ScriptableObject
    {
        [SerializeField] 
        private KeyCode proneKeyCode;
        [SerializeField] 
        private KeyCode crouchKeyCode;
        [SerializeField] 
        private KeyCode runningKeyCode;
        [SerializeField] 
        private KeyCode jumpKeyCode;

        private float smoothHorizontal;
        private float smoothVertical;
        [SerializeField] private float smoothSpeed = 10f;

        public bool IsCrouch()
        {
            return UnityEngine.Input.GetKey(crouchKeyCode);
        }

        public bool IsProne()
        {
            return UnityEngine.Input.GetKey(proneKeyCode);
        }

        public bool IsRunning()
        {
            return UnityEngine.Input.GetKey(runningKeyCode);
        }

        public bool IsJumping()
        {
            return UnityEngine.Input.GetKeyDown(jumpKeyCode);
        }
        
        public bool AskToChangePose()
        {
            return IsProne() || IsCrouch();
        }

        public float GetHorizontal()
        {
            float target = UnityEngine.Input.GetAxisRaw("Horizontal");
            smoothHorizontal = Mathf.Lerp(smoothHorizontal, target, Time.deltaTime * smoothSpeed);
            return smoothHorizontal;
        }

        public float GetVertical()
        {
            float target = UnityEngine.Input.GetAxisRaw("Vertical");
            smoothVertical = Mathf.Lerp(smoothVertical, target, Time.deltaTime * smoothSpeed);
            return smoothVertical;
        }
    }
}