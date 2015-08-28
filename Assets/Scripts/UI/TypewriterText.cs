using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TypewriterText : MonoBehaviour
    {
        public Text TextBox;

        private string _text;
        private float _speed;

        private float _time;

        void Start()
        {
            TypeText("We'll SEE about that--BEHOLD, my GIANT DEATH LASER!  Finally, I will be able to have my revenge--AN EYE FOR AN EYE!");
        }

        void FixedUpdate()
        {
            _time += Time.deltaTime;

            if (_time >= _speed)
            {
                _time = 0;

                if (_text.Any())
                {
                    var text = _text[0];
                    TextBox.text += text;
                    _text = _text.Remove(0, 1);
                }
            }
        }

        private void TypeText(string text, float speed = .05f)
        {
            _text = text;
            TextBox.text = string.Empty;

            _speed = speed;
        }
    }
}