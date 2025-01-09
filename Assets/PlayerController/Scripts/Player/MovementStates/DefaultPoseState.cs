namespace Player.Movement.States
{
    public class DefaultPoseState : MovementState
    {
        public DefaultPoseState(MovementSharedValues sharedValues) : base(sharedValues)
        {
            
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            sharedValues.TargetHeight = sharedValues.OriginalHeight;
            sharedValues.TargetCenter = sharedValues.OriginalCenter;
        }

        public override void Tick()
        {
            sharedValues.CurrentSpeed = input.IsRunning() ? config.RunSpeed : config.Speed;

            base.Tick();
        }
    }
}