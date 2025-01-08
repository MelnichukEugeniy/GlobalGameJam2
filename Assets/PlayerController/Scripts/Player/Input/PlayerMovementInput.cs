using UnityEngine;

namespace Player.Input
{
    [CreateAssetMenu(fileName = nameof(PlayerMovementInput), menuName = "Player/MovementInput")]
    public class PlayerMovementInput : ScriptableObject
    {
        [SerializeField] private KeyCode _proneKeyCode;
        [SerializeField] private KeyCode _crouchKeyCode;
        [SerializeField] private KeyCode _runningKeyCode;
        [SerializeField] private KeyCode _jumpKeyCode;

        public bool IsCrouch()
        {
            return UnityEngine.Input.GetKey(_crouchKeyCode);
        }

        public bool IsProne()
        {
            return UnityEngine.Input.GetKey(_proneKeyCode);
        }

        public bool IsRunning()
        {
            return UnityEngine.Input.GetKey(_runningKeyCode);
        }

        public bool IsJumping()
        {
            return UnityEngine.Input.GetKeyDown(_jumpKeyCode);
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