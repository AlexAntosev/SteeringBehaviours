using Models;
using UnityEngine;

namespace Movement.Providers
{
    public abstract class DesiredVelocityProvider : MonoBehaviour
    {
        [SerializeField, Range(0, 10)]
        public float weight = 1;

        protected Creature creature;

        private void Awake()
        {
            creature = GetComponent<Creature>();
        }

        public abstract Vector3 GetDesiredVelocity();
    }
}
