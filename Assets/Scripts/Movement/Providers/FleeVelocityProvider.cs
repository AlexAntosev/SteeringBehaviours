using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Movement.Providers
{
    public class FleeVelocityProvider : DesiredVelocityProvider, IAffectiveVelocityProvider
    {
        public Creature NearestCreature { get; set; }

        public override Vector3 GetDesiredVelocity() =>
            IsNoAnotherCreatureNear() ? DoNothing() : Seek();

        private bool IsNoAnotherCreatureNear() => 
            NearestCreature == null || !NearestCreature.IsAlive;

        private Vector3 Seek()
        {
            var desiredVelocity = 
                -(NearestCreature.transform.position - transform.position).normalized * creature.VelocityLimit;

            return desiredVelocity;
        }
        
        private Vector3 DoNothing() => Vector3.zero;
    }
}
