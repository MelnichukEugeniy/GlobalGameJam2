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
            
            SharedValues.TargetHeight = SharedValues.OriginalHeight;
            SharedValues.TargetCenter = SharedValues.OriginalCenter;
        }

        public override void Tick()
        {
            SharedValues.CurrentSpeed = Input.IsRunning() ? Config.RunSpeed : Config.Speed;

            base.Tick();

            var modificator = input.IsRunning() ? 1 : .5f;
            sharedValues.PlayerAnimator.SetForwardVelocity(input.GetVertical() * modificator);
            sharedValues.PlayerAnimator.SetRightVelocity(input.GetHorizontal() * modificator);
        }
    }
}