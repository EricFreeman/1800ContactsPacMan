using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Messages;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour, IListener<LoadNextLevelMessage>
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
                if (string.IsNullOrEmpty(level))
                {
                    LoadNextLevel();
                }
                else
                {
                    LoadLevel(level);
                }
            }

            this.Register<LoadNextLevelMessage>();
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
        }

        public void LoadLevel(string levelName)
        {
            PlayerPrefs.SetString("Level", levelName);
            
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
            }
        }

        public void LoadCutscene(string cutsceneName, string levelName)
        {
            PlayerPrefs.SetString("Cutscene", cutsceneName);
            PlayerPrefs.SetString("Level", levelName);

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
            if (string.IsNullOrEmpty(currentLevel))
            {
                LoadLevel(LevelSequence.First(x => !x.IsCutscene()).PrefabName);
            }
            else
            {
                int index;
                if (Application.loadedLevelName == "Game")
                {
                    index = LevelSequence.IndexOf(LevelSequence.FirstOrDefault(x => x.PrefabName == currentLevel && !x.IsCutscene()));
                }
                else
                {
                    index = LevelSequence.IndexOf(LevelSequence.FirstOrDefault(x => x.ConversationName == currentCutscene && x.IsCutscene()));
                }

                if (index >= LevelSequence.Count)
                {
                    Application.LoadLevel("MainMenu");
                }

                var next = LevelSequence[index + 1];
                if (next.IsCutscene() && index + 1 < LevelSequence.Count)
                {
                    var nextLevel = LevelSequence[index + 1];

                    LoadCutscene(next.ConversationName, nextLevel.PrefabName);
                }
                else
                {
                    LoadLevel(next.PrefabName);
                }
            }
        }

        public void Handle(LoadNextLevelMessage message)
        {
            LoadNextLevel();
        }
    }
}