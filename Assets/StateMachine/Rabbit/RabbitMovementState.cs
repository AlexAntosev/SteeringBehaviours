namespace StateMachine.Rabbit
{
    public class RabbitMovementState : MovementState
    {
        protected RabbitMovementStateMachine rabbitMovementStateMachine;

        public RabbitMovementState(RabbitMovementStateMachine stateMachine) : base(stateMachine)
        {
            rabbitMovementStateMachine = stateMachine;
        }
    }
}