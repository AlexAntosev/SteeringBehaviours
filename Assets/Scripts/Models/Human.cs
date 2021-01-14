using UnityEngine;

namespace Models
{
    public class Human : Creature
    {
        public float angle;
        public void Update()
        {
                var positionTargetted = Vector3.zero;
                var mousePoint = Input.mousePosition;
                var ray = Camera.main.ScreenPointToRay(mousePoint);
                var plane = new Plane(Vector3.up, Vector3.zero);
                if (plane.Raycast(ray, out float distance))
                {
                    positionTargetted = ray.GetPoint(distance);
                }

                angle = Vector3.Angle(transform.forward.normalized, positionTargetted.normalized);
                var cross = Vector3.Cross(transform.forward.normalized, positionTargetted.normalized);
                if (cross.y < 0)
                {
                    angle = -angle;
                }

                transform.RotateAround(transform.position, transform.up, angle);
        }
    }
}
