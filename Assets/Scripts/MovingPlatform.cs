using UnityEngine;

namespace Assets.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;
        public float PlatformSpeed;

        private bool _movingTo;

        void Update()
        {
            if (_movingTo)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndPosition, PlatformSpeed * Time.deltaTime);
                if (transform.position == EndPosition)
                {
                    _movingTo = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPosition, PlatformSpeed * Time.deltaTime);
                if (transform.position == StartPosition)
                {
                    _movingTo = true;
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(StartPosition, EndPosition);
            Gizmos.DrawCube(StartPosition, new Vector3(4, 1, 3));
            Gizmos.DrawCube(EndPosition, new Vector3(4, 1, 3));
        }

        void OnCollisionEnter(Collision collision)
        {
            var col = collision.collider.transform;
            if (col.name == "Player")
            {
                col.SetParent(transform);
            }
        }

        void OnCollisionExit(Collision collision)
        {
            var col = collision.collider.transform;
            if (col.name == "Player")
            {
                col.SetParent(null);
            }
        }
    }
}