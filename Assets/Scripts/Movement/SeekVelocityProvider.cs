using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class SeekVelocityProvider : DesiredVelocityProvider
    {
        public Transform objectToFollow;

        public override Vector3 GetDesiredVelocity()
        {
            if (nearestCreature == null)
            {
                return Vector3.zero;
            }

            return Seek();
        }

        private Vector3 Seek()
        {
            var desiredVelocity = (nearestCreature.position - transform.position).normalized * creature.velocityLimit;

            return desiredVelocity;
        }
    }
}
