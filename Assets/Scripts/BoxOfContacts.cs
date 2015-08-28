using UnityEngine;

namespace Assets.Scripts
{
    public class BoxOfContacts : MonoBehaviour
    {
        public string NextLevelName;
        public string NextCutsceneName;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Player")
            {
                if (!string.IsNullOrEmpty(NextCutsceneName))
                {
                    PlayerPrefs.SetString("Level", NextLevelName);
                    PlayerPrefs.SetString("Cutscene", NextCutsceneName);
                    Application.LoadLevel("Cutscene");
                }
                else
                {
                    var levelManager = FindObjectOfType<LevelManager>();
                    levelManager.LoadLevel(NextLevelName);
                }
            }
        }
    }
}