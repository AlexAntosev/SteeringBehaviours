using Models;
using StateMachine.Common;

namespace StateMachine.Wolf
{
    public class WolfEnemyAppear : WolfMovementState
    {
        public WolfEnemyAppear(WolfMovementStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Move()
        {
            if (!wolfMovementStateMachine.Creature.IsAlive)
            {
                wolfMovementStateMachine.SetState(new Dead(_stateMachine));
            }
            
            var nearestCreature = wolfMovementStateMachine.FlairResolver.GetNearestCreature();

            SetVelocityProvidersWeight(nearestCreature);

            if (nearestCreature == null)
            {
                _stateMachine.SetState(new WolfDefault(wolfMovementStateMachine));
            }
        }

        private void SetVelocityProvidersWeight(Creature nearestCreature)
        {
            wolfMovementStateMachine.seekVelocityProvider.weight = 1.5f;
            wolfMovementStateMachine.seekVelocityProvider.NearestCreature = nearestCreature;
            wolfMovementStateMachine.wonderVelocityProvider.weight = 1f;
            wolfMovementStateMachine.avoidEdgesVelocityProvider.weight = 1.25f;
        }
    }
}