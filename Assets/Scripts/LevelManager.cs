using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Messages;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour, IListener<LoadNextLevelMessage>
    {
        [HideInInspector]
        public List<LevelSequence> LevelSequence;
        public bool IgnoreLevelLoad;

        private GameObject _currentLevel;

        void Start()
        {
            LevelSequence = InitializeLevelSequence();
            var level = PlayerPrefs.GetString("Level");

            if (!IgnoreLevelLoad)
            {
                if (string.IsNullOrEmpty(level))
                    LoadNextLevel();
                else
                    LoadLevel(level);
            }

            this.Register<LoadNextLevelMessage>();
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

            _currentLevel = Instantiate(Resources.Load<GameObject>("Prefabs/Levels/" + levelName));
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
            if (string.IsNullOrEmpty(currentLevel))
            {
                LoadLevel(LevelSequence.First(x => !x.IsCutscene()).PrefabName);
            }
            else
            {
                var index = LevelSequence.IndexOf(LevelSequence.FirstOrDefault(x => x.PrefabName == currentLevel && !x.IsCutscene()));
                if (index >= LevelSequence.Count)
                {
                    Application.LoadLevel("MainMenu");
                }

                var next = LevelSequence[index + 1];
                if (next.IsCutscene() && index + 2 < LevelSequence.Count)
                {
                    var nextLevel = LevelSequence[index + 2];

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