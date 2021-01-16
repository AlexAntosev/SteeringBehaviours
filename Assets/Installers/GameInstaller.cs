using Factories;
using Models;
using Pulls;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Bullet bullet;

        [SerializeField]
        private Rabbit rabbit;
        
        [SerializeField]
        private Wolf wolf;
        
        [SerializeField]
        private Deer deer;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<GenericPull>().AsSingle();
            // Container.BindFactory<Bullet, BulletFactory>().FromComponentInNewPrefab(bullet);
            Container.BindFactory<Bullet, BulletPullFactory>().FromComponentInNewPrefab(bullet);
            Container.BindFactory<Rabbit, RabbitFactory>().FromComponentInNewPrefab(rabbit);
            Container.BindFactory<Wolf, WolfFactory>().FromComponentInNewPrefab(wolf);
            Container.BindFactory<Deer, DeerFactory>().FromComponentInNewPrefab(deer);
        }
    }
}