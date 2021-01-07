using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Creature : MonoBehaviour
    {
        public Vector3 velocity;

        public Vector3 acceleration;

        public float mass = 1;

        public float velocityLimit = 3;

        public float steeringForceLimit = 5;

        public float epsilone = 0.5f;

        public void ApplyForce(Vector3 force)
        {
            force /= mass;
            acceleration += force;
        }

        public void Update()
        {
            ApplyFriction();

            ApplySteeringForce();

            ApplyForces();
        }

        private void ApplyFriction()
        {
            var friction = -velocity.normalized * 0.5f;
            ApplyForce(friction);
        }

        private void ApplySteeringForce()
        {
            var movementProvider = GetComponent<DesiredVelocityProvider>();
            if (movementProvider == null)
            {
                return;
            }

            var desiredVelocity = movementProvider.GetDesiredVelocity();
            var steeringForce = desiredVelocity - velocity;

            ApplyForce(steeringForce.normalized * steeringForceLimit);
        }

        private void ApplyForces()
        {
            velocity += acceleration * Time.deltaTime;

            velocity = Vector3.ClampMagnitude(velocity, velocityLimit);

            if (velocity.magnitude < epsilone)
            {
                velocity = Vector3.zero;
                return;
            }

            transform.position += velocity * Time.deltaTime;
            acceleration = Vector3.zero;
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
