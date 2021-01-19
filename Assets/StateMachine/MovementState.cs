namespace StateMachine
{
    public class MovementState : State
    {
        protected CreatureMovementStateMachine creatureMovementStateMachine;
        
        public MovementState(CreatureMovementStateMachine stateMachine) : base(stateMachine)
        {
            creatureMovementStateMachine = stateMachine;
        }
    }
}