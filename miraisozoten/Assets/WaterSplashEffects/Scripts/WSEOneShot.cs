using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterSplashEffects {
	public class WSEOneShot : MonoBehaviour {
		public float duration = 1.0f;
		float time = 0.0f;

		void Start() {
			Destroy(gameObject, duration);
			time = 0.0f;
		}

		void Update() {
			var rend = GetComponent<Renderer>();
			var num_frame = rend.sharedMaterial.GetInt("_numOfFrames");
			var props = new MaterialPropertyBlock();
			props.SetInt("_timeCount", Mathf.FloorToInt(time * (num_frame - 1.0f) / duration));
			rend.SetPropertyBlock(props);

			time += Time.deltaTime;
		}

	}
}

