using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Movement.Providers.Flock
{
    public class FlockCohesionVelocityProvider : DesiredVelocityProvider
    {
        public List<Creature> flock;
        
        [SerializeField, Range(0, 100)]
        private float neighborDistance = 50;
        
        public override Vector3 GetDesiredVelocity()
        {
            return Cohesion();
        }
        
        private Vector3 Cohesion()
        {
            var velocity = Vector3.zero;
            var nearFlockCreaturesCount = 0;
            
            foreach (var flockCreature in flock)
            {
                var distanceToFlockCreature = (transform.position - flockCreature.transform.position).magnitude;
                if (IsNearFlockCreature(distanceToFlockCreature))
                {
                    velocity += flockCreature.transform.position;
                    nearFlockCreaturesCount++;
                }
            }

            if (IsNoFlockNear(nearFlockCreaturesCount))
            {
                return Vector3.zero;
            }
            
            velocity /= nearFlockCreaturesCount;

            return SeekFlock(velocity);
        }

        private static bool IsNoFlockNear(int nearFlockCreaturesCount)
        {
            return nearFlockCreaturesCount <= 0;
        }

        private bool IsNearFlockCreature(float distanceToFlockCreature)
        {
            return (distanceToFlockCreature > 0) && (distanceToFlockCreature < neighborDistance);
        }

        private Vector3 SeekFlock(Vector3 objectToSeek)
        {
            var desiredVelocity = (objectToSeek - transform.position).normalized * creature.VelocityLimit;

            return desiredVelocity;
        }
    }
}
