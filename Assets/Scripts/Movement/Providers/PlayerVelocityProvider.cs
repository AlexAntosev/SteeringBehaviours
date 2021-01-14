using UnityEngine;

namespace Movement.Providers
{
    public class PlayerVelocityProvider : DesiredVelocityProvider
    {
        public override Vector3 GetDesiredVelocity()
        {
            var velocity = Vector3.zero;
            
            if (Input.GetKey("d"))
            {
                velocity = new Vector3(500 * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey("a"))
            {
                velocity = new Vector3(-500 * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey("w"))
            {
                velocity = new Vector3(0, 0, 500 * Time.deltaTime);
            }

            if (Input.GetKey("s"))
            {
                velocity = new Vector3(0, 0, -500 * Time.deltaTime);
            }

            return velocity;
        }
    }
}
