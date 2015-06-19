using UnityEngine;

public class SolutionDroplet : MonoBehaviour
{

	private void Start()
	{

	}

	private void Update()
	{

	}

	private void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
	}
}