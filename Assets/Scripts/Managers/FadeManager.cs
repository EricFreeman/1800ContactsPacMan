using System;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class FadeManager : MonoBehaviour, IListener<FadeOutLevelMessage>, IListener<FadeInLevelMessage>
    {
        private Action _callback;
        private Image _image;
        private float _fadeSpeed;
        private float _alpha;
        private bool _sentMessage;

        void Start()
        {
            _image = GetComponent<Image>();
            this.Register<FadeInLevelMessage>();
            this.Register<FadeOutLevelMessage>();
            _alpha = 1;
            EventAggregator.SendMessage(new FadeInLevelMessage());
        }

        void OnDestroy()
        {
            this.UnRegister<FadeInLevelMessage>();
            this.UnRegister<FadeOutLevelMessage>();
        }

        void Update()
        {
            if (_fadeSpeed != 0)
            {
                _alpha += _fadeSpeed;
                _image.color = new Color(0, 0, 0, _alpha);
                if ((_alpha >= 1 || _alpha < 0) && !_sentMessage)
                {
                    _sentMessage = true;
                    _fadeSpeed = 0;
                    if (_callback != null)
                    {
                        _callback();
                    }
                    EventAggregator.SendMessage(new FadeCompleteMessage());
                }
            }
        }

        public void Handle(FadeOutLevelMessage message)
        {
            _fadeSpeed = .1f;
            _callback = message.Callback;
            _sentMessage = false;
        }

        public void Handle(FadeInLevelMessage message)
        {
            _fadeSpeed = -.1f;
            _callback = message.Callback;
            _sentMessage = false;
        }
    }
}