using UnityEngine;

namespace Assets.Scripts
{
	public class Spinner : MonoBehaviour
	{
		public float Speed;

		void Update()
		{
			transform.Rotate(Vector3.forward * Speed * Time.deltaTime);
		}
	}
}
