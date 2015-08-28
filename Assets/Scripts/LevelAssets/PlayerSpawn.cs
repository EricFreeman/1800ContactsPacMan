using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.LevelAssets
{
    public class PlayerSpawn : MonoBehaviour, IListener<RespawnPlayerMessage>
    {
	    public List<AudioClip> RespawnAudioClips;

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

	        if (RespawnAudioClips.Count > 0)
	        {
		        var randomIndex = Random.Range(0, RespawnAudioClips.Count - 1);
		        var audioClip = RespawnAudioClips[randomIndex];
		        AudioSource.PlayClipAtPoint(audioClip, transform.position);
	        }
        }

        public void Handle(RespawnPlayerMessage message)
        {
            RespawnPlayer();
        }
    }
}