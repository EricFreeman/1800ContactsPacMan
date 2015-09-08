using UnityEngine;

namespace Assets.Scripts
{
	public class Accelerator : MonoBehaviour
	{
		void OnTriggerEnter(Collider collision)
		{
			if (collision.name == "Player")
			{
                collision.GetComponent<Rigidbody>().AddForce(transform.forward * 666);

                var audioClip = (AudioClip)Resources.Load("Audio/Accelerator", typeof(AudioClip));
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
			}
		}
	}
}