using Assets.Scripts.Extensions;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public float MaxSpeed;
        public float Acceleration;
        public float Deceleration;
        public float AirDeceleration;

        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            _rb.velocity += new Vector3(moveHorizontal, 0.0f, moveVertical) * Acceleration;
            _rb.velocity = _rb.velocity.Clamp(-MaxSpeed, MaxSpeed, true);

            if (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical) <= .1f)
            {
                var currentDeceleration = IsGrounded() ? Deceleration : AirDeceleration;
                var temp = _rb.velocity;
                temp.x = Mathf.MoveTowards(temp.x, 0, currentDeceleration);
                temp.z = Mathf.MoveTowards(temp.z, 0, currentDeceleration);

                _rb.velocity = temp;
            }

            if (transform.position.y <= -20 || Input.GetKeyDown(KeyCode.R))
            {
                EventAggregator.SendMessage(new RespawnPlayerMessage());
            }
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position - new Vector3(0, .5f, 0), Vector2.down, .1f);
        }
    }
}