﻿Shader "WaterSplashEffects/VertexAnimationParticleTransparent" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Transparency ("Transparency", Range(0, 1)) = 1.0
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_boundingMax("Bounding Max", Float) = 1.0
		_boundingMin("Bounding Min", Float) = 1.0
		_numOfFrames("Number Of Frames", int) = 240
		_posTex ("Position Map (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags {"RenderType"="Transparent" "Queue"="Transparent"}
		LOD 200
		ZWrite Off
				
		CGPROGRAM
		// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it uses non-square matrices
		#pragma exclude_renderers gles
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow vertex:vert alpha:fade
		#pragma target 3.0
		#pragma instancing_options procedural:vertInstancingSetup
		#define UNITY_PARTICLE_INSTANCE_DATA MyParticleInstanceData
		#define UNITY_PARTICLE_INSTANCE_DATA_NO_ANIM_FRAME
		struct MyParticleInstanceData
		{
			float3x4 transform;
			uint color;
			float lifetime;
		};

		#include "UnityCG.cginc"
		#include "UnityStandardParticleInstancing.cginc"

		sampler2D _posTex;
		uniform float _boundingMax;
		uniform float _boundingMin;
		uniform int _numOfFrames;

		struct Input {
			float2 uv_MainTex;
			float4 vcolor : COLOR;
		};

		struct appdata_custom {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 color : COLOR;
			float4 texcoord : TEXCOORD0;
			float2 texcoord1 : TEXCOORD1;
			float2 texcoord2 : TEXCOORD2;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		half4 _Color;
		half _Transparency;
		half _Glossiness;
		half _Metallic;

		//vertex function
		void vert(inout appdata_custom v){
			UNITY_SETUP_INSTANCE_ID(v);
			vertInstancingColor(v.color);
			//calculate uv coordinates
#if defined(UNITY_PARTICLE_INSTANCING_ENABLED)
			UNITY_PARTICLE_INSTANCE_DATA data = unity_ParticleInstanceData[unity_InstanceID];
			float t = data.lifetime;
#else
			float t = _Time.y;
#endif
			float timeInFrames = (ceil(frac(-t * 1.0f) * _numOfFrames))/_numOfFrames + (1.0/_numOfFrames);

			//get position, normal and colour from textures
			float4 texturePos = tex2Dlod(_posTex,float4(v.texcoord.x, (timeInFrames + v.texcoord.y), 0, 0));

			//expand normalised position texture values to world space
			float expand = _boundingMax - _boundingMin;
			texturePos.xyz *= expand;
			texturePos.xyz += _boundingMin;
			texturePos.x *= -1;  //flipped to account for right-handedness of unity
			v.vertex.xyz = texturePos.xzy;  //swizzle y and z because textures are exported with z-up

			//calculate normal
			//decode float to float2
			float alpha = texturePos.w * 1023;
			float2 f2;
			f2.x = floor(alpha / 32.0) / 31.0;
			f2.y = (alpha - (floor(alpha / 32.0)*32.0)) / 31.0;

			// Spheremap Transform
		    float4 nn = float4(f2, 0, 0) * float4(2,2,0,0) + float4(-1,-1,1,-1);
		    half l = dot(nn.xyz,-nn.xyw);
		    nn.z = l;
		    nn.xy *= sqrt(l);
		    float3 f3 = nn.xyz * 2 + half3(0,0,-1);
			f3 = clamp(f3, -1.0, 1.0);
			f3.x *= -1;
			v.normal = normalize(f3);

			//set vertex colour
			v.color.rgb *= _Color.rgb;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = IN.vcolor.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = IN.vcolor.a * _Transparency;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
