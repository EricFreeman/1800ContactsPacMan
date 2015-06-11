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
            }
        }
    }
}