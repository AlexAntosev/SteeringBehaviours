using UnityEngine;

namespace Models
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
            
            creatureToKill.Kill();
        }
    }
}
