using Assets.Scripts.Models;

namespace Assets.Scripts.Shooting
{
    public interface IBulletFactory
    {
        Bullet CreateBullet();

        void RemoveBullet(Bullet bullet);
    }
}
