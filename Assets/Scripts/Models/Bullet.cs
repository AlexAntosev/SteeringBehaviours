using System;
using UnityEngine;

namespace Models
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _velocity;

        private Vector3 _acceleration;
        
        [SerializeField, Range(0.01f, 10f)]
        private float mass = 0.01f;

        [SerializeField, Range(1, 1000)]
        private float velocityLimit = 100;

        private const float Epsilon = 0.5f;

        private Vector3 _direction;

        public event EventHandler OnStop;

        public void Construct(Vector3 position, Vector3 direction)
        {
            transform.position = position;
            _direction = direction;
            
            ApplyForce(_direction * velocityLimit);
        }

        public void Update()
        {
            Fly();

            if (_velocity == Vector3.zero)
            {
                OnStop?.Invoke(null, EventArgs.Empty);
            }
        }

        private void Fly()
        {
            ApplyFriction();
            
            ApplyForces();
        }

        public void OnCollisionEnter(Collision collision)
        {
            KillCreature(collision);
        }

        private void KillCreature(Collision collision)
        {
            var creatureToKill = collision.gameObject.GetComponent<Creature>();
            if (CanNotKillCreature(creatureToKill))
            {
                return;
            }

            creatureToKill.Kill();
            OnStop?.Invoke(null, EventArgs.Empty);
        }

        private static bool CanNotKillCreature(Creature creatureToKill) => 
            creatureToKill == null || !creatureToKill.IsAlive;

        private void ApplyForce(Vector3 force)
        {
            force /= mass;
            _acceleration += force;
        }
        
        private void ApplyFriction()
        {
            if (_velocity.magnitude > 0)
            {
                var friction = -_velocity.normalized * 0.01f;
                ApplyForce(friction);
            }
        }

        private void ApplyForces()
        {
            _velocity += _acceleration * Time.deltaTime;

            _velocity = Vector3.ClampMagnitude(_velocity, velocityLimit);

            if (_velocity.magnitude < Epsilon)
            {
                _velocity = Vector3.zero;
                return;
            }

            transform.position += _velocity * Time.deltaTime;
            _acceleration = Vector3.zero;
            transform.rotation = Quaternion.LookRotation(_velocity);
        }
    }
}
