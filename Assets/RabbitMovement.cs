using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    public Rigidbody rigidbody;

    void Start()
    {
        
    }

    void Update()
    {
        rigidbody.AddForce(0, 0, 1000 * Time.deltaTime);
    }
}
