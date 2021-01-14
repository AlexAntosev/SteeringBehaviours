using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Movement.Providers.Flock
{
    public class FlockAlignVelocityProvider : DesiredVelocityProvider
    {
        public List<Creature> flock;
        
        [SerializeField, Range(0, 100)]
        private float neighborDistance = 50;
        
        public override Vector3 GetDesiredVelocity()
        {
            return Align();
        }

        private Vector3 Align()
        {
            var velocity = Vector3.zero;
            var nearFlockCreaturesCount = 0;
            
            foreach (var flockCreature in flock)
            {
                var distanceToFlockCreature = (transform.position - flockCreature.transform.position).magnitude;
                if (IsNearFlockCreature(distanceToFlockCreature))
                {
                    velocity += flockCreature.Velocity;
                    nearFlockCreaturesCount++;
                }
            }

            if (IsNoFlockNear(nearFlockCreaturesCount))
            {
                return Vector3.zero;
            }
            
            velocity /= nearFlockCreaturesCount;
            var steeringForce = velocity.normalized * creature.VelocityLimit;

            return steeringForce;

        }

        private static bool IsNoFlockNear(int nearFlockCreaturesCount) => 
            nearFlockCreaturesCount <= 0;

        private bool IsNearFlockCreature(float distanceToFlockCreature) => 
            distanceToFlockCreature > 0 && distanceToFlockCreature < neighborDistance;
    }
}
