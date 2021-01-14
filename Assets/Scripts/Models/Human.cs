using UnityEngine;

namespace Models
{
    public class Human : Creature
    {
        public void Update()
        {
            base.Update();

            if (IsAlive)
            {
                RotateHumanWithCursor();
            }
        }

        private void RotateHumanWithCursor()
        {
            var cursorPosition = GetCursorPosition();

            var angle = GetRotationAngle(cursorPosition);

            transform.RotateAround(transform.position, transform.up, angle);
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

        private float GetRotationAngle(Vector3 cursorPosition)
        {
            var angle = Vector3.Angle(transform.forward, cursorPosition);
            var cross = Vector3.Cross(transform.forward, cursorPosition);
            if (cross.y < 0)
            {
                angle = -angle;
            }

            return angle;
        }
    }
}
