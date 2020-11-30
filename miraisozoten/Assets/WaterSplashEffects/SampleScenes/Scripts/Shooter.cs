using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterSplashEffects.SampleScene {
	public class Shooter : MonoBehaviour {
		public GameObject bullet;
		[Range(1, 5)]
		public float period;
		float current;

		// Use this for initialization
		void Start() {
			current = period;
		}

		// Update is called once per frame
		void Update() {
			current -= Time.deltaTime;

			if(current < 0.0f) {
				var b = Instantiate(bullet, this.transform.position, Quaternion.identity);
				b.GetComponent<Rigidbody>().velocity = new Vector3(-10.0f, 4.0f, 0);

				current = period;
			}

		}
	}
}