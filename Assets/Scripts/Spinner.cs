using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
