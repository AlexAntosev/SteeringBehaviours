using Factories;
using UnityEngine;
using Zenject;

namespace Models
{
    public class Shooter : MonoBehaviour
    {
        private BulletFactory _bulletFactory;

        [Inject]
        public void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public void Update()
        {
            var human = GetComponent<Human>();
            if (CanShoot(human))
            {
                Shoot();
            }
        }

        private static bool CanShoot(Human human) => 
            Input.GetMouseButtonDown(0) &&
            human != null &&
            human.IsAlive;

        private void Shoot()
        {
            var bullet = _bulletFactory.Create();

            var bulletPosition = GetBulletPosition();
            var bulletDirection = GetBulletDirection();

            bullet.Construct(bulletPosition, bulletDirection);
        }

        private Vector3 GetBulletDirection()
        {
            return transform.forward.normalized;
        }

        private Vector3 GetBulletPosition()
        {
            var bulletPosition = transform.position + transform.forward.normalized;
            return bulletPosition;
        }

        private static Vector3 GetCursorPosition()
        {
            var cursorPosition = Vector3.zero;
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, Vector3.zero);
            
            if (plane.Raycast(ray, out var distance))
            {
                cursorPosition = ray.GetPoint(distance);
            }

            return cursorPosition;
        }
    }
}
