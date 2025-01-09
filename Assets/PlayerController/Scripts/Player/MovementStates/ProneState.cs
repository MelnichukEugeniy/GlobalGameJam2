using UnityEngine;

namespace Player.Movement.States
{
    public class ProneState : MovementState
    {
        public ProneState(MovementSharedValues sharedValues) : base(sharedValues)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            sharedValues.TargetHeight = config.CrouchHeight;
            sharedValues.TargetCenter = config.CrouchCenter;
            sharedValues.CurrentSpeed = config.CrouchSpeed;
        }

        private float _timer;

        public override void Tick()
        {
            base.Tick();

            if (_timer > config.TransitionSpeed)
            {
                sharedValues.TargetHeight = config.ProneHeight;
                sharedValues.TargetCenter = config.ProneCenter;
                sharedValues.CurrentSpeed = config.ProneSpeed;
            }
            else
            {
                _timer += Time.deltaTime;
            }

            controller.hasModifiableContacts = true;
        }

        public override void OnExit()
        {
            base.OnExit();

            _timer = 0;
        }
    }
}