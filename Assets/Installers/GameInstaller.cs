using Assets.Scripts.Models;
using Assets.Scripts.Shooting;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Bullet bullet;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindFactory<Bullet, BulletFactory>().FromComponentInNewPrefab(bullet);
        }
    }
}