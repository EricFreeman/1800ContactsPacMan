using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class FadeManager : MonoBehaviour, IListener<FadeOutLevelMessage>, IListener<ResetFadeMessage>
    {
        private Image _image;
        private bool _isFading;
        private float _alpha;
        private bool _sentMessage;

        void Start()
        {
            _image = GetComponent<Image>();
            this.Register<FadeOutLevelMessage>();
            this.Register<ResetFadeMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<FadeOutLevelMessage>();
            this.UnRegister<ResetFadeMessage>();
        }

        void Update()
        {
            if (_isFading)
            {
                _alpha += .1f;
                _image.color = new Color(0, 0, 0, _alpha);
                if (_alpha >= 1 && !_sentMessage)
                {
                    _sentMessage = true;
                    EventAggregator.SendMessage(new LevelFadedOutMessage());
                }
            }
        }

        public void Handle(FadeOutLevelMessage message)
        {
            _isFading = true;
        }

        public void Handle(ResetFadeMessage message)
        {
            _isFading = false;
            _sentMessage = false;
            _alpha = 0;
            _image.color = new Color(0, 0, 0, 0);
        }
    }
}