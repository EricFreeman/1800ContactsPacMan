using UnityEngine;

namespace Assets.Scripts
{
    public class Spring : MonoBehaviour
    {
        void OnTriggerEnter(Collider collision)
        {
            if (collision.name == "Player")
            {
                var rigidBody = collision.GetComponent<Rigidbody>();
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                rigidBody.AddForce(0, 666, 0);

                var audioClip = (AudioClip) Resources.Load("Audio/jump", typeof (AudioClip));
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
        }
    }
}