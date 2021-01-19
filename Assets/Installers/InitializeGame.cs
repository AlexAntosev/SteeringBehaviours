using System.Collections.Generic;
using System.Linq;
using Factories;
using Flair;
using Models;
using Movement.Providers.Flock;
using Pulls;
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
        
        [SerializeField]
        private Bullet bulletPrefab;
        
        [SerializeField]
        private GameObject ground;

        private float minXAxis = -50;
        private float maxXAxis = 50;
        private float minZAxis = -50;
        private float maxZAxis = 50;
        
        private RabbitFactory _rabbitFactory;
        private WolfFactory _wolfFactory;
        private DeerFactory _deerFactory;

        private List<Rabbit> _rabbits;
        private List<Wolf> _wolves;
        private List<Deer> _deers;

        [SerializeField]
        private Human player;

        private GenericPull _pull;

        [Inject]
        public void Construct(
            RabbitFactory rabbitFactory,
            WolfFactory wolfFactory,
            DeerFactory deerFactory,
            GenericPull pull)
        {
            _rabbitFactory = rabbitFactory;
            _wolfFactory = wolfFactory;
            _deerFactory = deerFactory;

            _rabbits = new List<Rabbit>();
            _wolves = new List<Wolf>();
            _deers = new List<Deer>();

            _pull = pull;

            var localScale = ground.transform.localScale;
            minXAxis = -localScale.x / 2 + 10;
            maxXAxis = localScale.x / 2 - 10;
            minZAxis = -localScale.z / 2 + 10;
            maxZAxis = localScale.z / 2 - 10;
        }
        
        void Start()
        {
            CreateRabbits();
            CreateWolves();
            CreateDeers();

            SetupCreaturesFlair();

            SetupDeersFlocks();

            SetupPullPrefabs();
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

        private void SetupCreaturesFlair()
        {
            SetupRabbitsFlair();

            SetupWolvesFlair();
            
            SetupDeersFlair();
        }

        private void SetupWolvesFlair()
        {
            foreach (var wolf in _wolves)
            {
                var flairResolver = wolf.GetComponent<FlairResolver>();
                flairResolver.creaturesToFlair.AddRange(_rabbits);
                flairResolver.creaturesToFlair.AddRange(_deers);
                flairResolver.creaturesToFlair.Add(player);
            }
        }

        private void SetupRabbitsFlair()
        {
            foreach (var rabbit in _rabbits)
            {
                var flairResolver = rabbit.GetComponent<FlairResolver>();
                flairResolver.creaturesToFlair.AddRange(_wolves);
                flairResolver.creaturesToFlair.AddRange(_rabbits.Where(r => r != rabbit));
                flairResolver.creaturesToFlair.AddRange(_deers);
                flairResolver.creaturesToFlair.Add(player);
            }
        }
        
        private void SetupDeersFlair()
        {
            foreach (var deer in _deers)
            {
                var flairResolver = deer.GetComponent<FlairResolver>();
                flairResolver.creaturesToFlair.AddRange(_wolves);
                flairResolver.creaturesToFlair.Add(player);
            }
        }

        private void SetupDeersFlocks()
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
        
        
        private void SetupPullPrefabs()
        {
            _pull.prefabs.Add(bulletPrefab.gameObject);
        }
    }
}
