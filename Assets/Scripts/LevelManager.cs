using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject CurrentLevel;

        void Start()
        {
            string level = null; //PlayerPrefs.GetString("Level");
            if (string.IsNullOrEmpty(level))
            {
                // TODO: Get rid of hard coded level1
                level = "Playground";
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