namespace StateMachine.Wolf
{
    public class WolfMovementState : MovementState
    {
        protected WolfMovementStateMachine wolfMovementStateMachine;

        public WolfMovementState(WolfMovementStateMachine stateMachine) : base(stateMachine)
        {
            wolfMovementStateMachine = stateMachine;
        }
    }
}