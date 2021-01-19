using Flair;
using Models;

namespace StateMachine.Common
{
    public class EnemyAppear : State
    {
        public EnemyAppear(StateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override void Move()
        {
            var creature = _stateMachine.GetComponent<Creature>();
            if (creature == null)
            {
                return;
            }
            
            if (!creature.IsAlive)
            {
                _stateMachine.SetState(new Dead(_stateMachine));
            }
            
            var flairResolver = _stateMachine.GetComponent<FlairResolver>();
            if (flairResolver == null)
            {
                return;
            }

            var nearestCreature = flairResolver.GetNearestCreature();
            if (nearestCreature == null)
            {
                _stateMachine.SetState(new DefaultMovement(_stateMachine));
            }
        }
    }
}