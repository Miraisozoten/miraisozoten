using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterSplashEffects {
	[ExecuteInEditMode]
	public class WSELoop : MonoBehaviour {
		public float duration = 1.0f;

		void Start() {
		}

		void Update() {
			var rend = GetComponent<Renderer>();
			var num_frame = rend.sharedMaterial.GetInt("_numOfFrames");
			var props = new MaterialPropertyBlock();
			props.SetInt("_timeCount", Mathf.FloorToInt(Time.time * (num_frame - 1.0f) / duration));
			rend.SetPropertyBlock(props);
		}

	}
}

