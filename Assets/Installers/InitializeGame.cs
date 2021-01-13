using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Flair;
using Assets.Scripts.Models;
using Assets.Scripts.Movement.Providers.Flock;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InitializeGame : MonoBehaviour
    {
        [SerializeField]
        private int rabbitsCount;
        
        [SerializeField]
        private int wolvesCount;
        
        [SerializeField]
        private int deersCount;

        private int minXAxis = -15;
        private int maxXAxis = 15;
        private int minZAxis = -15;
        private int maxZAxis = 15;
        
        private RabbitFactory _rabbitFactory;
        private WolfFactory _wolfFactory;
        private DeerFactory _deerFactory;

        private List<Rabbit> _rabbits;
        private List<Wolf> _wolves;
        private List<Deer> _deers;

        [Inject]
        public void Construct(RabbitFactory rabbitFactory, WolfFactory wolfFactory, DeerFactory deerFactory)
        {
            _rabbitFactory = rabbitFactory;
            _wolfFactory = wolfFactory;
            _deerFactory = deerFactory;

            _rabbits = new List<Rabbit>();
            _wolves = new List<Wolf>();
            _deers = new List<Deer>();
        }
        
        void Start()
        {
            CreateRabbits();
            CreateWolves();
            CreateDeers();

            SetupFlair();

            SetupFlock();
        }

        private void CreateRabbits()
        {
            for (var i = 0; i < rabbitsCount; i++)
            {
                var rabbit = _rabbitFactory.Create();
                _rabbits.Add(rabbit);
                ChangeCreaturePosition(rabbit);
            }
        }
        
        private void CreateWolves()
        {
            for (var i = 0; i < wolvesCount; i++)
            {
                var wolf = _wolfFactory.Create();
                _wolves.Add(wolf);
                ChangeCreaturePosition(wolf);
            }
        }
        
        private void CreateDeers()
        {
            for (var i = 0; i < deersCount; i++)
            {
                var deer = _deerFactory.Create();
                _deers.Add(deer);
                ChangeCreaturePosition(deer);
            }
        }

        private void ChangeCreaturePosition(Creature creature)
        {
            var x = Random.Range(minXAxis, maxXAxis);
            var y = 0;
            var z = Random.Range(minZAxis, maxZAxis);

            creature.transform.position = new Vector3(x, y, z);
        }

        private void SetupFlair()
        {
            foreach (var rabbit in _rabbits)
            {
                var flairResolver = rabbit.GetComponent<FlairResolver>();
                flairResolver.targets.AddRange(_wolves);
                flairResolver.targets.AddRange(_rabbits.Where(r => r != rabbit));
                flairResolver.targets.AddRange(_deers);
            }
            
            foreach (var wolf in _wolves)
            {
                var flairResolver = wolf.GetComponent<FlairResolver>();
                flairResolver.targets.AddRange(_rabbits);
                flairResolver.targets.AddRange(_deers);
            }
        }

        private void SetupFlock()
        {
            foreach (var deer in _deers)
            {
                var flockAlignVelocityProvider = deer.GetComponent<FlockAlignVelocityProvider>();
                flockAlignVelocityProvider.flock = new List<Creature>();
                flockAlignVelocityProvider.flock.AddRange(_deers.Where(d => d != deer));

                var flockCohesionVelocityProvider = deer.GetComponent<FlockCohesionVelocityProvider>();
                flockCohesionVelocityProvider.flock = new List<Creature>();
                flockCohesionVelocityProvider.flock.AddRange(_deers.Where(d => d != deer));
                
                var flockSeparateVelocityProvider = deer.GetComponent<FlockSeparateVelocityProvider>();
                flockSeparateVelocityProvider.flock = new List<Creature>();
                flockSeparateVelocityProvider.flock.AddRange(_deers.Where(d => d != deer));
            }
        }
    }
}
