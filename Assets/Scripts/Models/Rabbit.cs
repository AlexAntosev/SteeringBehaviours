using Assets.Scripts.Flair;
using Assets.Scripts.Movement.Builders;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Rabbit : Creature
    {
        private Transform _nearestTarget;

        public void Update()
        {
            GetNearestTarget();

            base.Update();
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

        protected override Vector3 GetDesiredVelocity()
        {
            var desiredVelocityBuilder = GetComponent<DesiredVelocityBuilder>();
            if (desiredVelocityBuilder == null)
            {
                return Vector3.zero;
            }

            desiredVelocityBuilder = desiredVelocityBuilder
                .AddWonder(() => _nearestTarget == null)
                .AddFlee(() => _nearestTarget != null, _nearestTarget);

            var desiredVelocity = desiredVelocityBuilder.Build();

            return desiredVelocity;
        }
    }
}
