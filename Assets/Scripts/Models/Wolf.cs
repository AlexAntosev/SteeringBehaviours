using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Wolf : Creature
    {
        public void OnCollisionEnter(Collision collision)
        {
            var creatureToKill = collision.gameObject.GetComponent<Creature>();
            if (creatureToKill == null)
            {
                return;
            }
            creatureToKill.isAlive = false;
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;

        }
    }
}
