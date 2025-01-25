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
            return UnityEngine.Input.GetAxisRaw("Horizontal");
        }

        public float GetVertical()
        {
            return UnityEngine.Input.GetAxisRaw("Vertical");
        }
    }
}