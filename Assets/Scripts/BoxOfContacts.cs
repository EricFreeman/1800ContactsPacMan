using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts
{
    public class BoxOfContacts : MonoBehaviour
    {
        float? _nextLevelTime = null;

        private bool _sentMessage;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Player")
            {
                var audioClip = Resources.Load<AudioClip>("Audio/jingle");
                AudioSource.PlayClipAtPoint(audioClip, transform.position);

                var player = collision.collider.gameObject;
                player.GetComponent<Player>().IsAscending = true;

                _nextLevelTime = Time.fixedTime + 2f;
                EventAggregator.SendMessage(new LevelCompleteMessage());
            }
        }
        
        void FixedUpdate()
        {
            if (_nextLevelTime.HasValue && Time.fixedTime >= _nextLevelTime && !_sentMessage)
            {
                EventAggregator.SendMessage(new LoadNextLevelMessage());
                _sentMessage = true;
            }
        }
    }
}