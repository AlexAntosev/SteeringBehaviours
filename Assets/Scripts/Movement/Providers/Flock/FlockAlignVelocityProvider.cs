using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Movement.Providers.Flock
{
    public class FlockAlignVelocityProvider : DesiredVelocityProvider
    {
        public List<Creature> flock;
        public override Vector3 GetDesiredVelocity()
        {
            return Align();
        }

        public Vector3 Align()
        {
            float neighbordist = 50;
            var sum = Vector3.zero;
            int count = 0;
            foreach (var flockCreature in flock)
            {
                float distance = (transform.position - flockCreature.transform.position).magnitude;
                if ((distance > 0) && (distance < neighbordist))
                {
                    sum += flockCreature.Velocity;
                    count++;
                }
            }
            if (count > 0)
            {
                sum /= count;
                var steer = sum.normalized * creature.VelocityLimit;

                return steer;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}
