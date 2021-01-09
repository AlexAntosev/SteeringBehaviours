using Assets.Scripts.Flair;
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

        private Transform _nearestTarget;

        public void ApplyForce(Vector3 force)
        {
            force /= mass;
            acceleration += force;
        }

        public void Update()
        {
            ApplyFriction();

            GetNearestTarget();

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
            var desiredVelocity = GetDesiredVelocity();
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

        protected virtual Vector3 GetDesiredVelocity()
        {
            var movementProviders = GetComponents<DesiredVelocityProvider>();
            if (movementProviders == null)
            {
                return Vector3.zero;
            }

            var desiredVelocity = Vector3.zero;

            foreach (var movementProvider in movementProviders)
            {
                movementProvider.nearestCreature = _nearestTarget;
                desiredVelocity += movementProvider.GetDesiredVelocity();
            }

            return desiredVelocity;
        }

        public void GetNearestTarget()
        {
            var flairResolver = GetComponent<FlairResolver>();
            if (flairResolver == null)
            {
                return;
            }

            _nearestTarget = flairResolver.GetNearestTarget();     
        }
    }
}
