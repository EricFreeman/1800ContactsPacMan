using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
	public class LaserSpawner : MonoBehaviour
	{
		public GameObject Laser;
		public float MinimumSpawnTime;
		public float MaximumSpawnTime;

		private float _nextSpawnTime;

		void Start()
		{
			SetNextSpawnTime();
		}

		private void SetNextSpawnTime()
		{
			var randomInterval = Random.Range(MinimumSpawnTime, MaximumSpawnTime);
			_nextSpawnTime = Time.fixedTime + randomInterval;
		}

		void Update()
		{
			if (Time.fixedTime >= _nextSpawnTime)
			{
				Instantiate(Laser, transform.position, Quaternion.identity);
				SetNextSpawnTime();
			}
		}
	}
}