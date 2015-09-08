using System.Collections;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts
{
    public class BoxOfContacts : MonoBehaviour
    {
        float? _nextLevelTime = null;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Player")
            {
                var audioClip = (AudioClip)Resources.Load("Audio/jingle", typeof(AudioClip));
                AudioSource.PlayClipAtPoint(audioClip, transform.position);

                var player = collision.collider.gameObject;
                ((Player)player.GetComponent("Player")).IsAscending = true;

                _nextLevelTime = Time.fixedTime + 2f;
            }
        }
        
        void FixedUpdate()
        {
            if (_nextLevelTime.HasValue && Time.fixedTime >= _nextLevelTime)
            {
                EventAggregator.SendMessage(new LoadNextLevelMessage());
            }
        }
    }
}