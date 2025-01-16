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
            
            SharedValues.TargetHeight = Config.CrouchHeight;
            SharedValues.TargetCenter = Config.CrouchCenter;
            SharedValues.CurrentSpeed = Config.CrouchSpeed;
        }
    }
}