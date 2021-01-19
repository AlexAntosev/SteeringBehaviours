using Movement.Providers;

namespace StateMachine.Common
{
    public class Dead : State
    {
        public Dead(StateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Move()
        {
            var velocityProviders = _stateMachine.GetComponents<DesiredVelocityProvider>();

            if (velocityProviders != null)
            {
                foreach (var velocityProvider in velocityProviders)
                {
                    velocityProvider.weight = 0;
                }
            }
        }
    }
}