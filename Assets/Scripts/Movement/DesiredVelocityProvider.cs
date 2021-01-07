using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Movement
{
    public abstract class DesiredVelocityProvider : MonoBehaviour
    {
        public float Weight = 1;

        protected Rabbit rabbit;

        private void Awake()
        {
            rabbit = GetComponent<Rabbit>();
        }

        public abstract Vector3 GetDesiredVelocity();
    }
}
