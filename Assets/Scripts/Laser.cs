using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class Laser : MonoBehaviour
	{
		public GameObject Explosion;
		public List<AudioClip> LaserSounds;

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
			var randomIndex = Random.Range(0, LaserSounds.Count - 1);
			var audioClip = LaserSounds[randomIndex];
			AudioSource.PlayClipAtPoint(audioClip, transform.position);

			Instantiate(Explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}