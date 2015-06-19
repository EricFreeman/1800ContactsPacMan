using UnityEngine;

namespace Assets.Scripts
{
    public class BoxOfContacts : MonoBehaviour
    {
        public string NextLevelName;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Player")
            {
                var levelManager = FindObjectOfType<LevelManager>();
                levelManager.LoadLevel(NextLevelName);
            }
        }
    }
}