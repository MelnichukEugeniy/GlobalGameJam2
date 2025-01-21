namespace Player.Movement.States
{
    public class CrouchState : MovementState
    {
        public CrouchState(MovementSharedValues sharedValues) : base(sharedValues)
        {
            
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            sharedValues.TargetHeight = config.CrouchHeight;
            sharedValues.TargetCenter = config.CrouchCenter;
            sharedValues.PlayerAnimator.SetCrouch(true);
        }

        public override void Tick()
        {
            sharedValues.CurrentSpeed = config.CrouchSpeed;
            base.Tick();
        }

        public override void OnExit()
        {
            base.OnExit();
            sharedValues.PlayerAnimator.SetCrouch(false);
        }
    }
}