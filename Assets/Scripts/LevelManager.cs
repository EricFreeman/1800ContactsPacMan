using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject CurrentLevel;

        void Start()
        {
            string level = PlayerPrefs.GetString("Level");
            if (string.IsNullOrEmpty(level))
            {
                // TODO: Get rid of hard coded level1
                level = "Level1";
            }

            LoadLevel(level);
        }

        public void LoadLevel(string levelName)
        {
            if (CurrentLevel != null)
            {
                Destroy(CurrentLevel);
            }

            CurrentLevel = Instantiate(Resources.Load<GameObject>("Prefabs/Levels/" + levelName));
        }
    }
}