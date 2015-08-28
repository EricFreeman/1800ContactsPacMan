using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts
{
    public class BoxOfContacts : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Player")
            {
                EventAggregator.SendMessage(new LoadNextLevelMessage());
            }
        }
    }
}