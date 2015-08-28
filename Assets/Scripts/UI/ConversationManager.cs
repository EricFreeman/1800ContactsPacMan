using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ConversationManager : MonoBehaviour
    {
        public TypewriterText Typewriter;

        private Conversation _conversation;

        void Start()
        {
            var manager = new XmlManager<Conversation>();
            _conversation = manager.Load("Assets/Conversations/Level1.txt");
        }

        void Update()
        {
            
        }
    }
}