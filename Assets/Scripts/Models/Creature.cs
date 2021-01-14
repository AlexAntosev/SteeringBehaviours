using Flair;
using Movement.Providers;
using UnityEngine;

namespace Models
{
    public class Creature : MonoBehaviour
    {
        public bool IsAlive { get; private set; } = true;

        private Vector3 _velocity;

        private Vector3 _acceleration;

        [SerializeField, Range(1, 10)]
        private float mass = 1;

        [SerializeField, Range(1, 10)]
        private float velocityLimit = 3;

        [SerializeField, Range(1, 10)]
        private float steeringForceLimit = 5;

        private const float Epsilone = 0.5f;
        
        public float VelocityLimit => velocityLimit;

        public Vector3 Velocity => _velocity;

        public void Update()
        {
            if (!IsAlive)
            {
                return;
            }
            
            Flair();

            ApplyFriction();

            ApplySteeringForce();

            ApplyForces();
        }
        
        public void Kill()
        {
            IsAlive = false;
            GetComponent<Renderer>().material.color = Color.red;
        }

        private void ApplyFriction()
        {
            var friction = -_velocity.normalized * 0.5f;
            ApplyForce(friction);
        }

        private void ApplySteeringForce()
        {
            var desiredVelocity = GetDesiredVelocity();
            var steeringForce = desiredVelocity - _velocity;

            ApplyForce(steeringForce.normalized * steeringForceLimit);
        }
        
        private void ApplyForce(Vector3 force)
        {
            force /= mass;
            _acceleration += force;
        }

        private void ApplyForces()
        {
            _velocity += _acceleration * Time.deltaTime;

            _velocity = Vector3.ClampMagnitude(_velocity, velocityLimit);

            if (_velocity.magnitude < Epsilone)
            {
                _velocity = Vector3.zero;
                return;
            }

            transform.position += _velocity * Time.deltaTime;
            _acceleration = Vector3.zero;
            transform.rotation = Quaternion.LookRotation(_velocity);
        }

        private Vector3 GetDesiredVelocity()
        {
            var movementProviders = GetComponents<DesiredVelocityProvider>();
            if (movementProviders == null)
            {
                return Vector3.zero;
            }

            var desiredVelocity = Vector3.zero;

            foreach (var movementProvider in movementProviders)
            {
                desiredVelocity += movementProvider.GetDesiredVelocity();
            }

            return desiredVelocity;
        }
        
        private void Flair()
        {
            var affectiveVelocityProviders = GetComponents<IAffectiveVelocityProvider>();
            if (affectiveVelocityProviders == null)
            {
                return;
            }

            foreach (var affectiveVelocityProvider in affectiveVelocityProviders)
            {
                affectiveVelocityProvider.NearestCreature = GetNearestCreature();
            }
        }

        private Creature GetNearestCreature()
        {
            var flairResolver = GetComponent<FlairResolver>();
            if (flairResolver == null)
            {
                return null;
            }

            var nearestCreature = flairResolver.GetNearestCreature();  
            
            return nearestCreature;
        }
    }
}
