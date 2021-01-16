using Models;
using Pulls;
using Zenject;

namespace Factories
{
    public class BulletPullFactory : PlaceholderFactory<Bullet>
    {
        private readonly GenericPull _pull;

        public BulletPullFactory(GenericPull pull)
        {
            _pull = pull;
        }
        
        public override Bullet Create()
        {
            var bullet = _pull.Get<Bullet>();

            bullet.OnStop += ((sender, args) => _pull.Return(bullet));

            return bullet;
        }
    }
}