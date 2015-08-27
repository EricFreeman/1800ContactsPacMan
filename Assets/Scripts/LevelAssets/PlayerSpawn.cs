using UnityEngine;

namespace Assets.Scripts.LevelAssets
{
    public class PlayerSpawn : MonoBehaviour
    {
        private Player _player;

        void Start()
        {
            _player = FindObjectOfType<Player>();
            RespawnPlayer();
        }

        public void RespawnPlayer()
        {
            _player.transform.position = transform.position;
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Input.ResetInputAxes();
        }
    }
}