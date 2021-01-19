using StateMachine.Common;

namespace StateMachine.Deer
{
    public class DeerDefault : DeerMovementState
    {
        public DeerDefault(DeerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Move()
        {
            if (!deerMovementStateMachine.Creature.IsAlive)
            {
                deerMovementStateMachine.SetState(new Dead(_stateMachine));
            }
            
            SetVelocityProvidersWeight();


            var nearestCreature = deerMovementStateMachine.FlairResolver.GetNearestCreature();
            if (nearestCreature != null)
            {
                _stateMachine.SetState(new DeerEnemyAppear(deerMovementStateMachine));
            }
        }

        private void SetVelocityProvidersWeight()
        {
            deerMovementStateMachine.fleeVelocityProvider.weight = 0f;
            deerMovementStateMachine.fleeVelocityProvider.NearestCreature = null;
            deerMovementStateMachine.flockAlignVelocityProvider.weight = 1.25f;
            deerMovementStateMachine.flockCohesionVelocityProvider.weight = 1.25f;
            deerMovementStateMachine.flockSeparateVelocityProvider.weight = 1.25f;
            deerMovementStateMachine.avoidEdgesVelocityProvider.weight = 1.35f;
        }
    }
}