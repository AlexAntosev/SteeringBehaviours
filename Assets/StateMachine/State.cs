namespace StateMachine
{
    public abstract class State
    {
        protected StateMachine _stateMachine;

        public State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Move()
        {
        }
    }
}