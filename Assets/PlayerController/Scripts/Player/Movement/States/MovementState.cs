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
            UpdateCollider();

            Move();
        }

        private float _percentage;
        private float _timer;

        private void UpdateCollider()
        {
            _timer += Time.deltaTime;
            _percentage = Mathf.InverseLerp(0, config.TransitionSpeed, _timer);
            controller.center = Vector3.Lerp(sharedValues.OriginalCenter, sharedValues.TargetCenter, _percentage);
            controller.height = Mathf.Lerp(sharedValues.OriginalHeight, sharedValues.TargetHeight, _percentage);
            
            //controller.center = Vector3.Lerp(controller.center, sharedValues.TargetCenter, Time.deltaTime * config.TransitionSpeed);
            //controller.height = Mathf.Lerp(controller.height, sharedValues.TargetHeight, Time.deltaTime * config.TransitionSpeed);
            
            //UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            var headLocalPosition = sharedValues.HeadTransform.localPosition;
            headLocalPosition.y = controller.height / 2f;
            sharedValues.HeadTransform.localPosition = headLocalPosition + config.HeadOffset;
        }
        
        private void Move()
        {
            float deltax = input.GetHorizontal() * currentSpeed;
            float deltaz = input.GetVertical() * currentSpeed;

            Vector3 movement = new Vector3(deltax, 0, deltaz);

            if (controller.isGrounded)
            {
                sharedValues.VerticalVelocity = -1f;
                sharedValues.PlayerAnimator.SetJump(false);
                if (input.IsJumping() && Mathf.Abs(controller.height - sharedValues.OriginalHeight) < 0.1f)
                {
                    sharedValues.PlayerAnimator.SetJump(true);
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
            _timer = 0;
        }

        public virtual void OnExit()
        {
            _timer = 0;
        }

        public virtual Color GizmoColor()
        {
            return Color.green;
        }
    }
}