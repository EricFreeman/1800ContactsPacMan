using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject CurrentLevel;

        public void LoadLevel(string levelName)
        {
            Destroy(CurrentLevel);
            CurrentLevel = Instantiate(Resources.Load<GameObject>("Prefabs/Levels/" + levelName));

            var player = FindObjectOfType<Player>();
            player.transform.position = new Vector3(0, 1.69f, 2.06f);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Input.ResetInputAxes();
        }
    }
}