using UnityEngine;

namespace Assets.Scripts.Movement.Providers
{
    public class FleeVelocityProvider : DesiredVelocityProvider
    {
        public Transform objectToFlee;

        public override Vector3 GetDesiredVelocity()
        {
            return Seek();
        }

        private Vector3 Seek()
        {
            var desiredVelocity = -(objectToFlee.position - transform.position).normalized * creature.velocityLimit;

            return desiredVelocity;
        }
    }
}
