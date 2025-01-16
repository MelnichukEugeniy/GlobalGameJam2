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
            
            SharedValues.TargetHeight = Config.ProneHeight;
            SharedValues.TargetCenter = Config.ProneCenter;
            SharedValues.CurrentSpeed = Config.ProneSpeed;
        }
    }
}