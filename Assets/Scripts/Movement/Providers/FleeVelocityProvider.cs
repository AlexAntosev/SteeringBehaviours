using UnityEngine;

namespace Assets.Scripts.Movement.Providers
{
    public class FleeVelocityProvider : DesiredVelocityProvider
    {
        public Transform objectToFlee;

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
            var desiredVelocity = -(nearestCreature.position - transform.position).normalized * creature.velocityLimit;

            return desiredVelocity;
        }
    }
}
