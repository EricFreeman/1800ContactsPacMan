using System.Collections.Generic;
using System.Linq;
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
                if(string.IsNullOrEmpty(level))
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
            return new List<LevelSequence>
            {
                new LevelSequence { PrefabName="Level1", IsCutscene = true },
                new LevelSequence { PrefabName="Level1", DisplayName = "Breaking the Eyes 1"},
                new LevelSequence { PrefabName="Level2", IsCutscene = true },
                new LevelSequence { PrefabName="Level2", DisplayName = "Breaking the Eyes 2" },
                new LevelSequence { PrefabName="Level3", IsCutscene = true },
                new LevelSequence { PrefabName="Level3", DisplayName = "Eye Am Watching You" },
				new LevelSequence { PrefabName="RampJumpAndRingLevel", DisplayName = "Their Eyes Were Watching Baud" },
                new LevelSequence { PrefabName="Level4", IsCutscene = true },
                new LevelSequence { PrefabName="Level4", DisplayName = "Between The H and J" },
                new LevelSequence { PrefabName="Level5", IsCutscene = true },
                new LevelSequence { PrefabName="Level5", DisplayName = "My Own Prism" },
                new LevelSequence { PrefabName="Level6", IsCutscene = true },
                new LevelSequence { PrefabName="Level6", DisplayName = "Last Sighting" },
                new LevelSequence { PrefabName="Finale", IsCutscene = true },
            };
        }

        private void LoadNextLevel()
        {
            var currentLevel = PlayerPrefs.GetString("Level");
            if (string.IsNullOrEmpty(currentLevel))
            {
                LoadLevel(LevelSequence.First(x => !x.IsCutscene).PrefabName);
            }
            else
            {
                var index = LevelSequence.IndexOf(LevelSequence.FirstOrDefault(x => x.PrefabName == currentLevel && !x.IsCutscene));
                if (index >= LevelSequence.Count)
                {
                    Application.LoadLevel("MainMenu");
                }

                var next = LevelSequence[index + 1];
                if (next.IsCutscene && index + 2 < LevelSequence.Count)
                {
                    var nextLevel = LevelSequence[index + 2];
                    LoadCutscene(next.PrefabName, nextLevel.PrefabName);
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

    public class LevelSequence
    {
        public string PrefabName;
        public string DisplayName;
        public bool IsCutscene;
    }
}