using UnityEngine;

namespace Models
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _velocity;

        private Vector3 _acceleration;
        
        [SerializeField, Range(0.01f, 10f)]
        private float mass = 0.01f;

        [SerializeField, Range(1, 10)]
        private float velocityLimit = 10;

        private const float Epsilon = 0.5f;

        private Vector3 _direction;

        public void Construct(Vector3 position, Vector3 direction)
        {
            transform.position = position;
            _direction = direction;
        }

        public void Update()
        {
            Fly();
        }

        private void Fly()
        {
            ApplyForce(_direction * velocityLimit);

            ApplyForces();
        }

        public void OnCollisionEnter(Collision collision)
        {
            KillCreature(collision);
        }

        private static void KillCreature(Collision collision)
        {
            var creatureToKill = collision.gameObject.GetComponent<Creature>();
            if (CanNotKillCreature(creatureToKill))
            {
                return;
            }

            creatureToKill.Kill();
        }

        private static bool CanNotKillCreature(Creature creatureToKill) => 
            creatureToKill == null || !creatureToKill.IsAlive;

        private void ApplyForce(Vector3 force)
        {
            force /= mass;
            _acceleration += force;
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
