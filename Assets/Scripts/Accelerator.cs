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
			}
		}
	}
}