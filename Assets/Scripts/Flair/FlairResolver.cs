using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

namespace Flair
{
    public class FlairResolver : MonoBehaviour
    {
        [SerializeField, Range(0, 100)]
        private float radius = 1;

        public List<Creature> creaturesToFlair;

        public Creature GetNearestCreature()
        {
            var distancesToAnotherCreatures = GetDistancesToAnotherCreatures();

            if (!distancesToAnotherCreatures.Any())
            {
                return null;
            }

            var nearestCreature = GetNearestCreature(distancesToAnotherCreatures);

            return nearestCreature;
        }

        private static Creature GetNearestCreature(Dictionary<Creature, float> distancesToAnotherCreatures)
        {
            var nearestTarget = distancesToAnotherCreatures.Aggregate((l, r) =>
                l.Value < r.Value ? l : r).Key;

            return nearestTarget;
        }

        private Dictionary<Creature, float> GetDistancesToAnotherCreatures()
        {
            var distancesToAnotherCreatures = new Dictionary<Creature, float>();
            foreach (var target in creaturesToFlair)
            {
                var distance = (transform.position - target.transform.position).magnitude;
                if (distance < radius)
                {
                    distancesToAnotherCreatures.Add(target, distance);
                }
            }

            return distancesToAnotherCreatures;
        }
    }
}
