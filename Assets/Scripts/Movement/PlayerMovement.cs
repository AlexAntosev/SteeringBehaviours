using UnityEngine;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (Input.GetKey("d"))
            {
                _rigidbody.AddForce(500 * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey("a"))
            {
                _rigidbody.AddForce(-500 * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey("w"))
            {
                _rigidbody.AddForce(0, 0, 500 * Time.deltaTime);
            }

            if (Input.GetKey("s"))
            {
                _rigidbody.AddForce(0, 0, -500 * Time.deltaTime);
            }
        }
    }
}
