using Models;
using StateMachine.Common;

namespace StateMachine.Deer
{
    public class DeerEnemyAppear : DeerMovementState
    {
        public DeerEnemyAppear(DeerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Move()
        {
            if (!deerMovementStateMachine.Creature.IsAlive)
            {
                deerMovementStateMachine.SetState(new Dead(_stateMachine));
            }
            
            var nearestCreature = deerMovementStateMachine.FlairResolver.GetNearestCreature();

            SetVelocityProvidersWeight(nearestCreature);

            if (nearestCreature == null)
            {
                _stateMachine.SetState(new DeerDefault(deerMovementStateMachine));
            }
        }

        private void SetVelocityProvidersWeight(Creature nearestCreature)
        {
            deerMovementStateMachine.fleeVelocityProvider.weight = 1.5f;
            deerMovementStateMachine.fleeVelocityProvider.NearestCreature = nearestCreature;
            deerMovementStateMachine.flockAlignVelocityProvider.weight = 0.75f;
            deerMovementStateMachine.flockCohesionVelocityProvider.weight = 0.75f;
            deerMovementStateMachine.flockSeparateVelocityProvider.weight = 0.75f;
            deerMovementStateMachine.avoidEdgesVelocityProvider.weight = 1.25f;
        }
    }
}