using Movement.Providers;

namespace StateMachine.Wolf
{
    public class WolfMovementStateMachine : CreatureMovementStateMachine
    {
        public SeekVelocityProvider seekVelocityProvider;
        
        public WonderVelocityProvider wonderVelocityProvider;
        
        public AvoidEdgesVelocityProvider avoidEdgesVelocityProvider;
        
        public void Start()
        {
            base.Start();
            
            seekVelocityProvider = GetComponent<SeekVelocityProvider>();
            if (seekVelocityProvider == null)
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

            SetState(new WolfDefault(this));
        }

        public void Update()
        {
            State.Move();
        }
    }
}