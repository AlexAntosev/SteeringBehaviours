using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class SeekVelocityProvider : DesiredVelocityProvider
    {
        public override Vector3 GetDesiredVelocity()
        {
            if (nearestCreature == null || nearestCreature.transform == null || !nearestCreature.isAlive)
            {
                return Vector3.zero;
            }

            return Seek();
        }

        private Vector3 Seek()
        {
            var desiredVelocity = (nearestCreature.transform.position - transform.position).normalized * creature.velocityLimit;

            return desiredVelocity;
        }
    }
}
