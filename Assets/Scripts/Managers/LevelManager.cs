using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Messages;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour, IListener<LoadNextLevelMessage>, IListener<LevelFadedOutMessage>
    {
        [HideInInspector]
        public List<LevelSequence> LevelSequence;
        public bool IgnoreLevelLoad;

        private GameObject _currentLevel;
        private bool _hasSentLevelLoadedMessage;

        void Start()
        {
            LevelSequence = InitializeLevelSequence();

            var level = PlayerPrefs.GetString("Level");

            if (!IgnoreLevelLoad)
            {
                if (!string.IsNullOrEmpty(level))
                {
                    LoadLevel(level);
                }
                else
                {
                    LoadNextLevel();
                }
            }

            this.Register<LoadNextLevelMessage>();
            this.Register<LevelFadedOutMessage>();
        }

        void Update()
        {
            if (!_hasSentLevelLoadedMessage)
            {
                EventAggregator.SendMessage(new LevelSequenceLoadedMessage {Levels = LevelSequence});
                _hasSentLevelLoadedMessage = true;
            }
        }

        void OnDestroy()
        {
            this.UnRegister<LoadNextLevelMessage>();
            this.UnRegister<LevelFadedOutMessage>();
        }

        public void LoadLevel(string levelName)
        {
            PlayerPrefs.SetString("Level", levelName);
            PlayerPrefs.SetString("Cutscene", null);
            
            if (_currentLevel != null)
            {
                Destroy(_currentLevel);
            }

            if (Application.loadedLevelName != "Game")
            {
                Application.LoadLevel("Game");
            }
            else
            {
                _currentLevel = Instantiate(Resources.Load<GameObject>("Prefabs/Levels/" + levelName));
                EventAggregator.SendMessage(new ResetFadeMessage());
            }
        }

        public void LoadCutscene(string cutsceneName)
        {
            PlayerPrefs.SetString("Cutscene", cutsceneName);
            PlayerPrefs.SetString("Level", null);
            Application.LoadLevel("Cutscene");
        }

        private List<LevelSequence> InitializeLevelSequence()
        {
            var manager = new XmlManager<LevelConfiguration>();
            return manager.Load("Assets/Configuration/LevelSequence.xml").Levels;
        }

        private void LoadNextLevel()
        {
            var currentLevel = PlayerPrefs.GetString("Level");
            var currentCutscene = PlayerPrefs.GetString("Cutscene");

            if (string.IsNullOrEmpty(currentLevel) && string.IsNullOrEmpty(currentCutscene))
            {
                LoadLevel(LevelSequence.First(x => !x.IsCutscene()).PrefabName);
            }
            else
            {
                var index = LevelSequence.IndexOf(LevelSequence.LastOrDefault(x => x.PrefabName == currentLevel || x.ConversationName == currentCutscene));

                if (index + 1 >= LevelSequence.Count)
                {
                    Application.LoadLevel("MainMenu");
                }

                var next = LevelSequence[index + 1];
                if (next.IsCutscene())
                {
                    LoadCutscene(next.ConversationName);
                }
                else
                {
                    LoadLevel(next.PrefabName);
                }
            }
        }

        public void Handle(LoadNextLevelMessage message)
        {
            EventAggregator.SendMessage(new FadeOutLevelMessage());
        }

        public void Handle(LevelFadedOutMessage message)
        {
            LoadNextLevel();
        }
    }
}