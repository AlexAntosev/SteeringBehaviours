using Movement.Providers;

namespace StateMachine.Rabbit
{
    public class RabbitMovementStateMachine : CreatureMovementStateMachine
    {
        public FleeVelocityProvider fleeVelocityProvider;
        
        public WonderVelocityProvider wonderVelocityProvider;
        
        public AvoidEdgesVelocityProvider avoidEdgesVelocityProvider;
        
        public void Start()
        {
            base.Start();
            
            fleeVelocityProvider = GetComponent<FleeVelocityProvider>();
            if (fleeVelocityProvider == null)
            {
                return;
            }
            
            wonderVelocityProvider = GetComponent<WonderVelocityProvider>();
            if (wonderVelocityProvider == null)
            {
                return;
            }
            
            avoidEdgesVelocityProvider = GetComponent<AvoidEdgesVelocityProvider>();
            if (avoidEdgesVelocityProvider == null)
            {
                return;
            }

            SetState(new RabbitDefault(this));
        }

        public void Update()
        {
            State.Move();
        }
    }
}