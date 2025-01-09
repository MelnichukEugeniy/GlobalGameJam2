using Player.Input;
using StateMachinePattern;
using UnityEngine;

namespace Player.Movement.States
{
    public abstract class MovementState : IState
    {
        protected float currentSpeed
        {
            get => sharedValues.CurrentSpeed;
            set => sharedValues.CurrentSpeed = value;
        }

        protected PlayerMovementConfig config => sharedValues.Config;
        protected PlayerMovementInput input => sharedValues.Input;
        protected CharacterController controller => sharedValues.CharacterController;

        protected MovementSharedValues sharedValues;
        
        public MovementState(MovementSharedValues sharedValues)
        {
            this.sharedValues = sharedValues;
        }

        public virtual void Tick()
        {
            controller.center = Vector3.Lerp(controller.center, sharedValues.TargetCenter, Time.deltaTime * config.TransitionSpeed);
            controller.height = Mathf.Lerp(controller.height, sharedValues.TargetHeight, Time.deltaTime * config.TransitionSpeed);

            float deltax = input.GetHorizontal() * currentSpeed;
            float deltaz = input.GetVertical() * currentSpeed;
            
            Vector3 movement = new Vector3(deltax, 0, deltaz);

            if (controller.isGrounded)
            {
                sharedValues.VerticalVelocity = -1f;

                if (input.IsJumping() && Mathf.Abs(controller.height - sharedValues.OriginalHeight) < 0.1f)
                {
                    sharedValues.VerticalVelocity = config.JumpForce;
                }
            }
            else
            {
                sharedValues.VerticalVelocity += config.Gravity * Time.deltaTime;
            }
            
            movement.y = sharedValues.VerticalVelocity;

            movement *= Time.deltaTime;
            movement = sharedValues.Transform.TransformDirection(movement);
            controller.Move(movement);
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