using UnityEngine;

namespace Assets.Scripts.Movement
{
    public class WonderVelocityProvider : DesiredVelocityProvider
    {
        private float circleDistance = 1;

        private float circleRadius = 2;

        private int angleChangeStep = 15;

        private int angle = 0;

        public override Vector3 GetDesiredVelocity()
        {
            if(nearestCreature != null)
            {
                return Vector3.zero;
            }

            return Wonder();
        }

        private Vector3 Wonder()
        {
            var random = Random.value;
            if (random < 0.5)
            {
                angle += angleChangeStep;
            }
            else
            {
                angle -= angleChangeStep;
            }

            var futurePosition = creature.transform.position + creature.velocity.normalized * circleDistance;
            var vector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            var desiredVelocity = (futurePosition + vector - transform.position).normalized * creature.velocityLimit;

            return desiredVelocity;
        }
    }
}
