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
                    button.GetComponentInChildren<Text>().text = level.DisplayName;
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