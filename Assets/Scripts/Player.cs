using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using Assets.Scripts.Messages;
using Assets.Scripts.PowerUps.Behaviors;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public List<Behavior> Behaviors = new List<Behavior>();
        public float MaxSpeed;
        public float Acceleration;
        public float Deceleration;
        public float AirDeceleration;
        public bool IsBouncy = false;
        public bool IsSticky = false; 


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
            UpdatePowerUps();
        }

        void UpdatePowerUps()
        {
            CheckForBuffRemoval();
            BouncyBuff();
        }

        void BouncyBuff()
        {
            Debug.Log(IsBouncy);
            if (IsBouncy && IsGrounded())
            {
                _rb.AddForce(0, 66, 0);
            }
        }

        void CheckForBuffRemoval()
        {
            for (int index = Behaviors.Count - 1; index >= 0; index--)
            {
                var behavior = Behaviors[index];
                if (DateTime.Now.Subtract(TimeSpan.FromSeconds(behavior.Duration)).CompareTo(behavior.TimeStamp) > 0)
                {
                    behavior.RemoveBuffFromPlayer(this);
                    Behaviors.Remove(behavior);
                    //Debug.Log(this.Acceleration);
                    //Debug.Log(this.MaxSpeed);
                    Debug.Log(this.IsBouncy);
                }
            }
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position - new Vector3(0, .5f, 0), Vector3.down, .1f);
        }

        public void AddPowerUp(Behavior behavior)
        {
            behavior.ApplyBuffToPlayer(this);
            Behaviors.Add(behavior);
            //Debug.Log(this.Acceleration);
            //Debug.Log(this.MaxSpeed);
            Debug.Log(this.IsBouncy);
        }
    }
}