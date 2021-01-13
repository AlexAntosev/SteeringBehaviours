using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Movement.Providers.Flock
{
    public class FlockCohesionVelocityProvider : DesiredVelocityProvider
    {
        public List<Creature> flock;
        public override Vector3 GetDesiredVelocity()
        {
            return Cohesion();
        }
        private Vector3 Cohesion()
        {
            float neighbordist = 50;
            var sum = Vector3.zero;
            int count = 0;
            foreach (var flockCreature in flock)
            {
                float distance = (transform.position - flockCreature.transform.position).magnitude;
                if ((distance > 0) && (distance < neighbordist))
                {
                    sum += flockCreature.transform.position;
                    count++;
                }
            }
            if (count > 0)
            {
                sum /= count;

                return Seek(sum);
            }
            else
            {
                return Vector3.zero;
            }
        }

        private Vector3 Seek(Vector3 objectToSeek)
        {
            var desiredVelocity = (objectToSeek - transform.position).normalized * creature.VelocityLimit;

            return desiredVelocity;
        }
    }
}
