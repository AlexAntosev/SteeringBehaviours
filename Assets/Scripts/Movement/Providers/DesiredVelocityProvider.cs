using Models;
using UnityEngine;

namespace Movement.Providers
{
    public abstract class DesiredVelocityProvider : MonoBehaviour
    {
        public float Weight = 1;

        protected Creature creature;

        private void Awake()
        {
            creature = GetComponent<Creature>();
        }

        public abstract Vector3 GetDesiredVelocity();
    }
}
