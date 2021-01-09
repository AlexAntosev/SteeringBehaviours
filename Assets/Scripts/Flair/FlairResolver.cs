using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Flair
{
    public class FlairResolver : MonoBehaviour
    {
        [SerializeField, Range(0, 100)]
        private float radius = 1;

        public List<Creature> targets;

        public Creature GetNearestCreature()
        {
            var targetDistances = new Dictionary<Creature, float>();
            foreach (var target in targets)
            {
                var distance = (transform.position - target.transform.position).magnitude;
                if (distance < radius)
                {
                    targetDistances.Add(target, distance);
                }                
            }

            if (!targetDistances.Any())
            {
                return null;
            }

            var nearestTarget = targetDistances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;

            return nearestTarget;
        }
    }
}
