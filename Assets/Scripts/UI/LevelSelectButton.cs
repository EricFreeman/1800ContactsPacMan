using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LevelSelectButton : MonoBehaviour
    {
        public string LevelName;
        public Text Text;

        void Start()
        {
            Text.text = LevelName;
        }

        public void SelectLevel()
        {
            Application.LoadLevel("Game");
        }
    }
}