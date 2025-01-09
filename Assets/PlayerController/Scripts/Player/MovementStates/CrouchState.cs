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
            sharedValues.CurrentSpeed = config.CrouchSpeed;
        }
    }
}