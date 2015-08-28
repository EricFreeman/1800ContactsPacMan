using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Messages;
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
                    LoadCutscene(next.PrefabName, nextLevel.PrefabName);
                }
                else
                {
                    PlayerPrefs.SetString("Level", next.PrefabName);
                    LoadLevel(next.PrefabName);
                }
            }
        }

        public void Handle(LoadNextLevelMessage message)
        {
            LoadNextLevel();
        }
    }

    public class LevelConfiguration
    {
        public List<LevelSequence> Levels;
    }

    public class LevelSequence
    {
        public string PrefabName;
        public string ConversationName;
        public string DisplayName;

        public bool IsCutscene()
        {
            return string.IsNullOrEmpty(PrefabName);
        }
    }
}