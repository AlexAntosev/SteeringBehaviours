using Movement.Providers;
using Movement.Providers.Flock;

namespace StateMachine.Deer
{
    public class DeerMovementStateMachine : CreatureMovementStateMachine
    {
        public FlockAlignVelocityProvider flockAlignVelocityProvider;
        
        public FlockCohesionVelocityProvider flockCohesionVelocityProvider;
        
        public FlockSeparateVelocityProvider flockSeparateVelocityProvider;
        
        public FleeVelocityProvider fleeVelocityProvider;
        
        public AvoidEdgesVelocityProvider avoidEdgesVelocityProvider;
        
        public void Start()
        {
            base.Start();
            
            flockAlignVelocityProvider = GetComponent<FlockAlignVelocityProvider>();
            if (flockAlignVelocityProvider == null)
            {
                return;
            }
            
            flockCohesionVelocityProvider = GetComponent<FlockCohesionVelocityProvider>();
            if (flockCohesionVelocityProvider == null)
            {
                return;
            }
            
            flockSeparateVelocityProvider = GetComponent<FlockSeparateVelocityProvider>();
            if (flockSeparateVelocityProvider == null)
            {
                return;
            }

            avoidEdgesVelocityProvider = GetComponent<AvoidEdgesVelocityProvider>();
            if (avoidEdgesVelocityProvider == null)
            {
                return;
            }
            
            fleeVelocityProvider = GetComponent<FleeVelocityProvider>();
            if (fleeVelocityProvider == null)
            {
                return;
            }

            SetState(new DeerDefault(this));
        }

        public void Update()
        {
            State.Move();
        }
    }
}