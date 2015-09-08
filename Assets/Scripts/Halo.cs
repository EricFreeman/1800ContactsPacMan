using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Halo : MonoBehaviour
    {
        void FixedUpdate()
        {
            var player = GetComponentInParent<Player>();
            if (player.IsAscending)
            {
                foreach (var childRenderer in GetComponentsInChildren<Renderer>())
                {
                    childRenderer.enabled = true;
                }
            }
            else
            {
                foreach (var childRenderer in GetComponentsInChildren<Renderer>())
                {
                    childRenderer.enabled = false;
                }
            }
        }
    }
}
