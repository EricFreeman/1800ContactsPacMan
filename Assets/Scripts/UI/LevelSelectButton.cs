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
            PlayerPrefs.SetString("Cutscene", null);
            Application.LoadLevel("Game");
        }

        public void SelectCutscene()
        {
            PlayerPrefs.SetString("Cutscene", CutsceneName);
            PlayerPrefs.SetString("Level", null);
            Application.LoadLevel("Cutscene");
        }
    }
}