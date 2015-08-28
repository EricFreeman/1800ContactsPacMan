using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ConversationManager : MonoBehaviour
    {
        public TypewriterText Typewriter;
        public Text SpeakerName;
        public List<GameObject> Images;
        public AudioSource AudioSource;

        private Conversation _conversation;

        void Start()
        {
            var manager = new XmlManager<Conversation>();
            _conversation = manager.Load("Assets/Conversations/Level6.txt");
        }

        void Update()
        {
            if (!AudioSource.isPlaying && !_conversation.IsComplete)
            {
                var conversation = _conversation.Lines[_conversation.CurrentLine];
                Typewriter.TypeText(conversation.Text);
                SpeakerName.text = conversation.Name;
                AudioSource.clip = Resources.Load<AudioClip>("Audio/" + conversation.Audio);
                AudioSource.Play();

                Images.ForEach(x => x.SetActive(false));
                Images.First(x => x.name == conversation.Image).SetActive(true);

                _conversation.CurrentLine++;
            }
        }
    }
}