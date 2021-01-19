using Flair;
using Models;

namespace StateMachine
{
    public class CreatureMovementStateMachine : StateMachine
    {
        public Creature Creature;

        public FlairResolver FlairResolver;
        
        
        public void Start()
        {
            Creature = GetComponent<Creature>();
            if (Creature == null)
            {
                return;
            }
            
            FlairResolver = GetComponent<FlairResolver>();
            if (FlairResolver == null)
            {
                return;
            }
        }

        public void Update()
        {
            State.Move();
        }
    }
}