using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LevelSelectButton : MonoBehaviour
    {
        public string LevelName;
        public string CutsceneName;

        public Image Image;

        public void SelectLevel()
        {
            PlayerPrefs.SetString("Level", LevelName);
            Application.LoadLevel("Game");
        }

        public void SelectCutscene()
        {
            PlayerPrefs.SetString("Level", LevelName);
            PlayerPrefs.SetString("Cutscene", CutsceneName);
            Application.LoadLevel("Cutscene");
        }
    }
}