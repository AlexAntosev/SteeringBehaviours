using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Movement.Providers.Flock
{
    class FlockSeparateVelocityProvider : DesiredVelocityProvider
    {
        public List<Creature> flock;
        public override Vector3 GetDesiredVelocity()
        {
            return Separate();
        }

        private Vector3 Separate()
        {
            var r = 1; //creature size
            float desiredSeparation = r * 2;
            var sum = Vector3.zero;
            int count = 0;
            foreach (var flockCreature in flock)
            {
                float distance = (transform.position - flockCreature.transform.position).magnitude;
                if ((distance > 0) && (distance < desiredSeparation))
                {
                    var diff = (transform.position - flockCreature.transform.position).normalized;
                    diff /= distance;
                    sum += diff;
                    count++;
                }
            }
            if (count > 0)
            {
                sum /= count;
                var normalized = sum.normalized;
                var steer = normalized * creature.VelocityLimit;
                return steer;
            }

            return Vector3.zero;
        }
    }
}
