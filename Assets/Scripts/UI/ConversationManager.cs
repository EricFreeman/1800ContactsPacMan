using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Messages;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

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
            var convo = PlayerPrefs.GetString("Cutscene");
            if (string.IsNullOrEmpty(convo))
            {
                convo = "Level6";
            }

            var manager = new XmlManager<Conversation>();
            _conversation = manager.Load(string.Format("Assets/Conversations/{0}.txt", convo));
        }

        void Update()
        {
            if (Input.anyKeyDown)
            {
                AudioSource.Stop();
            }

            if (!AudioSource.isPlaying && !_conversation.IsComplete)
            {
                var conversation = _conversation.Lines[_conversation.CurrentLine];
                Typewriter.TypeText(conversation.Text);
                SpeakerName.text = conversation.Name;
                AudioSource.clip = Resources.Load<AudioClip>("Audio/" + conversation.Audio);
                AudioSource.Play();

                Images.ForEach(x => x.SetActive(false));
                var image = Images.FirstOrDefault(x => x.name == conversation.Image);
                if (image != null)
                {
                    image.SetActive(true);
                }

                _conversation.CurrentLine++;
            }

            if (!AudioSource.isPlaying && _conversation.IsComplete)
            {
                EventAggregator.SendMessage(new LoadNextLevelMessage());
                //Application.LoadLevel(string.IsNullOrEmpty(PlayerPrefs.GetString("Level")) ? "MainMenu" : "Game");
            }
        }
    }
}