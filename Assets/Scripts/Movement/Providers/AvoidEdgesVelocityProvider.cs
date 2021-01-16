using UnityEngine;

namespace Movement.Providers
{
    public class AvoidEdgesVelocityProvider : DesiredVelocityProvider
    {
        [SerializeField]
        private GameObject Ground;
        
        private float edge = 0.05f;

        public override Vector3 GetDesiredVelocity()
        {
            var range = transform.position - Ground.transform.position;
            var leftProjectedRange = Vector3.Project(range, Ground.transform.right);
            var localScale = Ground.transform.localScale;
            var rangeToRightEdge = new Vector3(localScale.x / 2, 0, 0) - leftProjectedRange;
            var rangeToLeftEdge = new Vector3(-localScale.x / 2, 0, 0) - leftProjectedRange;
            var upProjectedRange = Vector3.Project(range, Ground.transform.forward);
            var rangeToUpEdge = new Vector3(0, 0, localScale.z / 2) - upProjectedRange;
            var rangeToDownEdge = new Vector3(0, 0, -localScale.z / 2) - upProjectedRange;
            var maxSpeed = creature.VelocityLimit;
            var velocity = creature.Velocity;

            if (rangeToRightEdge.magnitude < 10)
            {
                return new Vector3(-maxSpeed, 0, 0);

            }
            if (rangeToLeftEdge.magnitude < 10)
            {
                return new Vector3(maxSpeed, 0, 0);
            }
            if (rangeToUpEdge.magnitude < 10)
            {
                return new Vector3(0, 0, -maxSpeed);
            }
            if (rangeToDownEdge.magnitude < 10)
            {
                return new Vector3(0, 0, maxSpeed);
            }

            return velocity;
        }
    }
}
