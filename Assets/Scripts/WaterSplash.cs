using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaterSplash : MonoBehaviour
    {
        void OnTriggerEnter(Collider collision)
        {
            if (collision.name == "Player")
            {
                var audioClip = (AudioClip)Resources.Load("Audio/splash", typeof(AudioClip));
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
        }
    }
}
