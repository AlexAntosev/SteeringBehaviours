using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Flair
{
    public class FlairResolver : MonoBehaviour
    {
        [SerializeField, Range(0, 100)]
        private float radius = 1;

        public List<Transform> targets;

        public Transform GetNearestTarget()
        {
            var targetDistances = new Dictionary<Transform, float>();
            foreach (var target in targets)
            {
                var distance = (transform.position - target.position).magnitude;
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
