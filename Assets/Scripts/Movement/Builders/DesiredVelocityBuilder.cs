using Assets.Scripts.Movement.Providers;
using System;
using UnityEngine;

namespace Assets.Scripts.Movement.Builders
{
    public class DesiredVelocityBuilder : MonoBehaviour
    {
        private Vector3 _desiredVelocity;

        public DesiredVelocityBuilder AddWonder(Func<bool> condition)
        {
            if (!condition())
            {
                return this;
            }

            var movementProvider = GetComponent<WonderVelocityProvider>();
            if (movementProvider == null)
            {
                return this;
            }

            _desiredVelocity = movementProvider.GetDesiredVelocity();

            return this;
        }

        public DesiredVelocityBuilder AddSeek(Func<bool> condition, Transform objectToFollow)
        {
            if (!condition())
            {
                return this;
            }

            var movementProvider = GetComponent<SeekVelocityProvider>();
            if (movementProvider == null)
            {
                return this;
            }

            movementProvider.objectToFollow = objectToFollow;

            _desiredVelocity = movementProvider.GetDesiredVelocity();

            return this;
        }

        public DesiredVelocityBuilder AddFlee(Func<bool> condition, Transform objectToFlee)
        {
            if (!condition())
            {
                return this;
            }

            var movementProvider = GetComponent<FleeVelocityProvider>();
            if (movementProvider == null)
            {
                return this;
            }

            movementProvider.objectToFlee = objectToFlee;

            _desiredVelocity = movementProvider.GetDesiredVelocity();

            return this;
        }

        public Vector3 Build()
        {
            return _desiredVelocity;
        }
    }
}
