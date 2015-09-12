using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class TimerManager : MonoBehaviour, IListener<FadeCompleteMessage>, IListener<LevelCompleteMessage>
    {
        private Text _textField;
        private float _time;
        private bool _startCountdown;

        void Start()
        {
            _textField = GetComponent<Text>();

            this.Register<FadeCompleteMessage>();
            this.Register<LevelCompleteMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<FadeCompleteMessage>();
            this.UnRegister<LevelCompleteMessage>();
        }

        void Update ()
        {
            if (_startCountdown)
            {
                _time += Time.deltaTime;
            }

            _textField.text = _time.ToString("F2");
        }

        public void Handle(FadeCompleteMessage message)
        {
            _startCountdown = true;
            _time = 0f;
        }

        public void Handle(LevelCompleteMessage message)
        {
            _startCountdown = false;
        }
    }
}