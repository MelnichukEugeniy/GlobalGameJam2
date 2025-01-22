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
            
            sharedValues.TargetHeight = config.ProneHeight;
            sharedValues.TargetCenter = config.ProneCenter;
            
            sharedValues.PlayerAnimator.SetProne(true);
        }

        public override void Tick()
        {
            sharedValues.CurrentSpeed = config.ProneSpeed;
            
            base.Tick();
        }

        public override void OnExit()
        {
            base.OnExit();
            
            sharedValues.PlayerAnimator.SetProne(false);
        }
    }
}