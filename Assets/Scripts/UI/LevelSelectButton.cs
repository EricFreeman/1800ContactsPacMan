using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

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
            EventAggregator.SendMessage(new LoadSelectedLevelMessage
            {
                Level = LevelName
            });
        }

        public void SelectCutscene()
        {
            PlayerPrefs.SetString("Cutscene", CutsceneName);
            PlayerPrefs.SetString("Level", null);
            EventAggregator.SendMessage(new LoadSelectedLevelMessage
            {
                Level = CutsceneName
            });
        }
    }
}