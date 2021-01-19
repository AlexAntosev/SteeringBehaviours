using Models;
using StateMachine.Common;

namespace StateMachine.Rabbit
{
    public class RabbitEnemyAppear : RabbitMovementState
    {
        public RabbitEnemyAppear(RabbitMovementStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Move()
        {
            if (!rabbitMovementStateMachine.Creature.IsAlive)
            {
                rabbitMovementStateMachine.SetState(new Dead(_stateMachine));
            }
            
            var nearestCreature = rabbitMovementStateMachine.FlairResolver.GetNearestCreature();

            SetVelocityProvidersWeight(nearestCreature);

            if (nearestCreature == null)
            {
                _stateMachine.SetState(new RabbitDefault(rabbitMovementStateMachine));
            }
        }

        private void SetVelocityProvidersWeight(Creature nearestCreature)
        {
            rabbitMovementStateMachine.fleeVelocityProvider.weight = 1.5f;
            rabbitMovementStateMachine.fleeVelocityProvider.NearestCreature = nearestCreature;
            rabbitMovementStateMachine.wonderVelocityProvider.weight = 1f;
            rabbitMovementStateMachine.avoidEdgesVelocityProvider.weight = 1.25f;
        }
    }
}