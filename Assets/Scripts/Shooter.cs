using Assets.Scripts.Models;
using Assets.Scripts.Shooting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
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
            if (Input.GetMouseButtonDown(0))
            {
                var bullet = _bulletFactory.Create();
                bullet.transform.position = transform.position;
                var positionTargetted = Vector3.zero;
                var mousePoint = Input.mousePosition;
                var ray = Camera.main.ScreenPointToRay(mousePoint);
                var plane = new Plane(Vector3.up, Vector3.zero);
                if (plane.Raycast(ray, out float distance))
                {
                    positionTargetted = ray.GetPoint(distance);
                }

                var direction = positionTargetted.normalized;
                bullet.Setup(direction);
            }
        }
    }
}
