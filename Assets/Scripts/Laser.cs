using UnityEngine;

namespace Assets.Scripts
{
	public class Laser : MonoBehaviour
	{
		public GameObject Explosion;

		private float _concentrationSpeed = 0.3f;
		
		void Update()
		{
			var scaleReduction = _concentrationSpeed*Time.deltaTime;
			transform.localScale -= new Vector3(scaleReduction, scaleReduction, scaleReduction);
			if (transform.localScale.x <= 0)
			{
				Explode();
			}
		}

		private void Explode()
		{
			Instantiate(Explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}