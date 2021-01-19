using StateMachine.Common;

namespace StateMachine.Wolf
{
    public class WolfDefault : WolfMovementState
    {
        public WolfDefault(WolfMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Move()
        {
            if (!wolfMovementStateMachine.Creature.IsAlive)
            {
                wolfMovementStateMachine.SetState(new Dead(_stateMachine));
            }
            
            SetVelocityProvidersWeight();


            var nearestCreature = wolfMovementStateMachine.FlairResolver.GetNearestCreature();
            if (nearestCreature != null)
            {
                _stateMachine.SetState(new WolfEnemyAppear(wolfMovementStateMachine));
            }
        }

        private void SetVelocityProvidersWeight()
        {
            wolfMovementStateMachine.seekVelocityProvider.weight = 0f;
            wolfMovementStateMachine.seekVelocityProvider.NearestCreature = null;
            wolfMovementStateMachine.wonderVelocityProvider.weight = 1.25f;
            wolfMovementStateMachine.avoidEdgesVelocityProvider.weight = 1.35f;
        }
    }
}