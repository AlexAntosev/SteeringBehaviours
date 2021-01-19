using StateMachine.Common;

namespace StateMachine.Rabbit
{
    public class RabbitDefault : RabbitMovementState
    {
        public RabbitDefault(RabbitMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Move()
        {
            if (!rabbitMovementStateMachine.Creature.IsAlive)
            {
                rabbitMovementStateMachine.SetState(new Dead(_stateMachine));
            }
            
            SetVelocityProvidersWeight();


            var nearestCreature = rabbitMovementStateMachine.FlairResolver.GetNearestCreature();
            if (nearestCreature != null)
            {
                _stateMachine.SetState(new RabbitEnemyAppear(rabbitMovementStateMachine));
            }
        }

        private void SetVelocityProvidersWeight()
        {
            rabbitMovementStateMachine.fleeVelocityProvider.weight = 0f;
            rabbitMovementStateMachine.fleeVelocityProvider.NearestCreature = null;
            rabbitMovementStateMachine.wonderVelocityProvider.weight = 1.25f;
            rabbitMovementStateMachine.avoidEdgesVelocityProvider.weight = 1.35f;
        }
    }
}