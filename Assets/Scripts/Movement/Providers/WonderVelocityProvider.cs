using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Movement.Providers
{
    public class WonderVelocityProvider : DesiredVelocityProvider, IAffectiveVelocityProvider
    {
        private float _circleDistance = 1;

        private float _circleRadius = 2;

        private int _angleChangeStep = 15;

        private int _angle = 0;
        
        public Creature NearestCreature { get; set; }

        public override Vector3 GetDesiredVelocity() =>
            IsNoAnotherCreatureNear() ? Wonder() : DoNothing();

        private bool IsNoAnotherCreatureNear() =>
            NearestCreature == null || !NearestCreature.IsAlive;
        
        private Vector3 Wonder()
        {
            var random = Random.value;
            if (random < 0.5)
            {
                _angle += _angleChangeStep;
            }
            else
            {
                _angle -= _angleChangeStep;
            }

            var futurePosition = creature.transform.position + creature.Velocity.normalized * _circleDistance;
            var vector = new Vector3(
                Mathf.Cos(_angle * Mathf.Deg2Rad),
                0,
                Mathf.Sin(_angle * Mathf.Deg2Rad));

            var desiredVelocity = (futurePosition + vector - transform.position).normalized * creature.VelocityLimit;

            return desiredVelocity;
        }
        
        private Vector3 DoNothing() => Vector3.zero;
    }
}
