using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour, IListener<LevelSequenceLoadedMessage>
    {
        public GameObject LevelSelectButton;
        public GameObject ConversationSelectButton;

        public Transform ScrollContent;

        void Start()
        {
            this.Register<LevelSequenceLoadedMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<LevelSequenceLoadedMessage>();
        }

        public void Handle(LevelSequenceLoadedMessage message)
        {
            foreach (var level in message.Levels)
            {
                var button = CreateButton(level.IsCutscene());
                button.LevelName = level.PrefabName;
                button.CutsceneName = level.ConversationName;

                if (!level.IsCutscene())
                {
                    button.transform.GetChild(0).GetComponentInChildren<Text>().text = level.DisplayName;

                    var besTimeText = button.transform.GetChild(2);
                    var bestTime = PlayerPrefs.GetFloat(level.PrefabName + "_bestScore");
                    if (bestTime > 0)
                    {
                        besTimeText.GetComponent<Text>().text = string.Format("Best Time: {0}", bestTime.ToString("F2"));
                    }
                    else
                    {
                        besTimeText.gameObject.SetActive(false);
                    }

                    button.transform.GetChild(0).GetComponentInChildren<Text>().text = level.DisplayName;
                    button.Image.sprite =
                        Resources.Load<Sprite>(string.Format("Images/LevelScreenshots/{0}",
                            string.IsNullOrEmpty(level.Image) ? "NoImage" : level.Image));
                }

                button.transform.SetParent(ScrollContent.transform, false);
            }
        }

        private LevelSelectButton CreateButton(bool isCutscene)
        {
            return Instantiate(isCutscene ? ConversationSelectButton : LevelSelectButton).GetComponent<LevelSelectButton>();
        }
    }
}