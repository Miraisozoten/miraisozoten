using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterSplashEffects.SampleScene {
	public class Bullet : MonoBehaviour {
		public GameObject hitEffect;
		public bool enableHitEffect = true;
		public float duration = 3.0f;
		public float explosionDuration = 5.0f;

		void Start() {
			Destroy(gameObject, duration);
			Invoke("Explode", duration * 0.95f);
		}

		void Update() {

		}

		void Explode() {
			var m = hitEffect.GetComponent<ParticleSystem>().main;
			m.loop = false;
			m.duration = explosionDuration;
			m.simulationSpeed = 10.0f;
			var h = Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(h, explosionDuration);
		}

	}
}
