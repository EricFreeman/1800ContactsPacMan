using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.LevelAssets
{
    public class PlayerSpawn : MonoBehaviour, IListener<RespawnPlayerMessage>
    {
        private Player _player;

        void Start()
        {
            _player = FindObjectOfType<Player>();
            RespawnPlayer();
            this.Register<RespawnPlayerMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<RespawnPlayerMessage>();
        }

        public void RespawnPlayer()
        {
            _player.transform.position = transform.position;
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Input.ResetInputAxes();
        }

        public void Handle(RespawnPlayerMessage message)
        {
            RespawnPlayer();
        }
    }
}