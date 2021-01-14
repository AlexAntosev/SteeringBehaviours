using UnityEngine;

namespace Models
{
    public class Wolf : Creature
    {
        public void OnCollisionEnter(Collision collision)
        {
            if (IsAlive)
            {
                KillCreature(collision);
            }
        }

        private static void KillCreature(Collision collision)
        {
            var creatureToKill = collision.gameObject.GetComponent<Creature>();
            if (CanNotKillCreature(creatureToKill))
            {
                return;
            }

            creatureToKill.Kill();
        }

        private static bool CanNotKillCreature(Creature creatureToKill) =>
            creatureToKill == null ||
            !creatureToKill.IsAlive ||
            creatureToKill.TryGetComponent(typeof(Wolf), out var wolf);
    }
}
