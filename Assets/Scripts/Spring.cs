using UnityEngine;

namespace Assets.Scripts
{
    public class Spring : MonoBehaviour
    {
        void OnTriggerEnter(Collider collision)
        {
            if (collision.name == "Player")
            {
                collision.GetComponent<Rigidbody>().AddForce(0, 666, 0);

                var audioClip = (AudioClip) Resources.Load("Audio/jump", typeof (AudioClip));
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
        }
    }
}