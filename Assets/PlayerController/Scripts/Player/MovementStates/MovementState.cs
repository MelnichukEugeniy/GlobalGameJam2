using Player.Input;
using StateMachinePattern;
using UnityEngine;

namespace Player.Movement.States
{
    public abstract class MovementState : IState
    {
        protected float CurrentSpeed
        {
            get => SharedValues.CurrentSpeed;
            set => SharedValues.CurrentSpeed = value;
        }

        protected PlayerMovementConfig Config => SharedValues.Config;
        protected PlayerMovementInput Input => SharedValues.Input;
        protected CapsuleCollider Collider => SharedValues.Collider;
        protected Rigidbody Rigidbody => SharedValues.Rigidbody;

        protected MovementSharedValues SharedValues;
        
        public MovementState(MovementSharedValues sharedValues)
        {
            SharedValues = sharedValues;
        }

        public virtual void Tick()
        {
            // Плавне оновлення висоти та центру
            Collider.center = Vector3.Lerp(Collider.center, SharedValues.TargetCenter, Time.deltaTime * Config.TransitionSpeed);
            Collider.height = Mathf.Lerp(Collider.height, SharedValues.TargetHeight, Time.deltaTime * Config.TransitionSpeed);

            // Рух
            float deltax = Input.GetHorizontal() * SharedValues.CurrentSpeed;
            float deltaz = Input.GetVertical() * SharedValues.CurrentSpeed;

            Vector3 movement = new Vector3(deltax, 0, deltaz);

            // Стрибок
            if (Input.IsJumping() && Mathf.Abs(Collider.height - SharedValues.OriginalHeight) < 0.1f)
            {
                if (IsGrounded())
                {
                    Debug.Log("Jump");
                    // Стрибок можливий тільки у звичайному стані
                    Rigidbody.AddForce(Vector3.up * Config.JumpForce, ForceMode.Impulse);
                }
            }

            movement = Vector3.ClampMagnitude(movement, SharedValues.CurrentSpeed);

            movement *= Time.deltaTime;
            movement = SharedValues.Transform.TransformDirection(movement);
            movement.y = Rigidbody.linearVelocity.y;
            Rigidbody.linearVelocity = movement;
        }

        protected bool IsGrounded()
        {
            var ray = new Ray(SharedValues.Transform.position - SharedValues.OriginalCenter, -SharedValues.Transform.up);
            bool result = Physics.Raycast(ray, out RaycastHit _, SharedValues.OriginalHeight / 1.9f, Config.GroundMask, QueryTriggerInteraction.Ignore);
            
            return result;
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual Color GizmoColor()
        {
            return Color.green;
        }
    }
}