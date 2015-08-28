using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.PowerUps.Behaviors;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    public class StickyPowerUp : MonoBehaviour
    {
        public List<Behavior> Behaviors = new List<Behavior> { new Sticky() };
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            Destroy(this.gameObject);

            if (other.name == "Player")
            {
                foreach (var behavior in Behaviors)
                {
                    other.GetComponent<Player>().AddPowerUp(behavior);
                }
            }
        }
    }
}
