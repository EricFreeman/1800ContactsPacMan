﻿using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public float Speed;
        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            _rb.AddForce(movement * Speed);

            if (transform.position.y <= -20 || Input.GetKeyDown(KeyCode.R))
            {
                EventAggregator.SendMessage(new RespawnPlayerMessage());
            }
        }
    }
}