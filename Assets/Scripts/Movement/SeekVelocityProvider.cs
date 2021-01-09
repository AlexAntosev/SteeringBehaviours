using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class SeekVelocityProvider : DesiredVelocityProvider
    {
        public Transform objectToFollow;

        public override Vector3 GetDesiredVelocity()
        {
            return Seek();
        }

        private Vector3 Seek()
        {
            var desiredVelocity = (objectToFollow.position - transform.position).normalized * creature.velocityLimit;

            return desiredVelocity;
        }
    }
}
