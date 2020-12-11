using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterSplashEffects.SampleScene {
	public class Splash : MonoBehaviour {
		public GameObject hitEffect;
		public bool enableHitEffect = true;
		public float duration = 3.0f;

		void Start() {

		}

		void Update() {

		}

		void OnCollisionEnter(Collision c) {
			if(enableHitEffect) {
				var ps = hitEffect.GetComponent<ParticleSystem>();
				var m = ps.main;
				m.loop = false;
				m.duration = duration;
				m.simulationSpeed = 10.0f;
				m.startRotation3D = true;
				var ang = Quaternion.FromToRotation(Vector3.up, -c.contacts[0].normal).eulerAngles;
				m.startRotationX = new ParticleSystem.MinMaxCurve(ang.x / 360.0f * 2.0f * Mathf.PI);
				m.startRotationY = new ParticleSystem.MinMaxCurve(ang.y / 360.0f * 2.0f * Mathf.PI);
				m.startRotationZ = new ParticleSystem.MinMaxCurve(ang.z / 360.0f * 2.0f * Mathf.PI);
				m.startSize = 1.5f;
				var h = Instantiate(hitEffect, c.contacts[0].point, Quaternion.identity);
				Destroy(h, duration);
			}
		}
	}
}
