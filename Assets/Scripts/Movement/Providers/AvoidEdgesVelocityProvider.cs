using UnityEngine;

namespace Assets.Scripts.Movement.Providers
{
    public class AvoidEdgesVelocityProvider : DesiredVelocityProvider
    {
        private float edge = 0.05f;

        public override Vector3 GetDesiredVelocity()
        {
            var camera = Camera.current;
            var maxSpeed = creature.velocityLimit;
            var velocity = creature.velocity;
            if (camera == null)
            {
                return velocity;
            }

            var point = camera.WorldToViewportPoint(transform.position);

            if (point.x > 1 - edge)
            {
                return new Vector3(-maxSpeed, 0, 0);

            }
            if (point.x < edge)
            {
                return new Vector3(maxSpeed, 0, 0);
            }
            if (point.y > 1 - edge)
            {
                return new Vector3(0, 0, -maxSpeed);
            }
            if (point.y < edge)
            {
                return new Vector3(0, 0, maxSpeed);
            }

            return velocity;
        }
    }
}
