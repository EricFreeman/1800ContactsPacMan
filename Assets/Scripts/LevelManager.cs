using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject CurrentLevel;

        void Start()
        {
            var level = PlayerPrefs.GetString("Level");
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

            var player = FindObjectOfType<Player>();
            player.transform.position = new Vector3(0, 1.69f, 2.06f);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Input.ResetInputAxes();
        }
    }
}