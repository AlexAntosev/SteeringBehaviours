using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Movement.Providers.Flock
{
    class FlockSeparateVelocityProvider : DesiredVelocityProvider
    {
        public List<Creature> flock;

        [SerializeField, Range(0, 100)]
        private float desiredSeparationDistance = 20;
        
        public override Vector3 GetDesiredVelocity()
        {
            return Separate();
        }

        private Vector3 Separate()
        {
            var velocity = Vector3.zero;
            var nearFlockCreaturesCount = 0;

            foreach (var flockCreature in flock)
            {
                float distanceToFlockCreature = (transform.position - flockCreature.transform.position).magnitude;
                if (IsNearFlockCreature(distanceToFlockCreature))
                {
                    var direction = (transform.position - flockCreature.transform.position).normalized;
                    direction /= distanceToFlockCreature;
                    
                    velocity += direction;
                    
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

        private static bool IsNoFlockNear(int nearFlockCreaturesCount)
        {
            return nearFlockCreaturesCount <= 0;
        }

        private bool IsNearFlockCreature(float distanceToFlockCreature)
        {
            return (distanceToFlockCreature > 0) && (distanceToFlockCreature < desiredSeparationDistance);
        }
    }
}
