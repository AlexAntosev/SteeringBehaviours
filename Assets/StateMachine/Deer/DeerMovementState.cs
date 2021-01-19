namespace StateMachine.Deer
{
    public class DeerMovementState : MovementState
    {
        protected DeerMovementStateMachine deerMovementStateMachine;

        public DeerMovementState(DeerMovementStateMachine stateMachine) : base(stateMachine)
        {
            deerMovementStateMachine = stateMachine;
        }
    }
}