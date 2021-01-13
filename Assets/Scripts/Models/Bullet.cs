using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _velocity;

        private Vector3 _acceleration;
        
        [SerializeField, Range(0.01f, 10f)]
        private float mass = 0.01f;

        [SerializeField, Range(1, 10)]
        private float velocityLimit = 10;

        private readonly float _epsilone = 0.5f;

        private Vector3 _direction;

        public void Setup(Vector3 direction)
        {
            _direction = direction;
        }

        public void Update()
        {
            ApplyForce(_direction * velocityLimit);
            
            ApplyForces();
        }
        
        public void OnCollisionEnter(Collision collision)
        {
            var creatureToKill = collision.gameObject.GetComponent<Creature>();
            if (creatureToKill == null)
            {
                return;
            }
            
            creatureToKill.Kill();
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

            if (_velocity.magnitude < _epsilone)
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
