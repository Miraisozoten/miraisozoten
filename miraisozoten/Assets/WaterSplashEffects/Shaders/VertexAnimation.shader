Shader "WaterSplashEffects/VertexAnimation" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_boundingMax("Bounding Max", Float) = 1.0
		_boundingMin("Bounding Min", Float) = 1.0
		_timeCount("Time Count", int) = 0
		_numOfFrames("Number Of Frames", int) = 240
		_posTex ("Position Map (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow vertex:vert
		#pragma target 3.0
		#pragma instancing_options procedural:vertInstancingSetup

		#include "UnityCG.cginc"
		#include "UnityStandardParticleInstancing.cginc"

		sampler2D _MainTex;
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
			float2 texcoord : TEXCOORD0;
			float2 texcoord1 : TEXCOORD1;
			float2 texcoord2 : TEXCOORD2;
			float4 color : COLOR;
			uint vertexId : SV_VertexID;
			uint instId : SV_InstanceID;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		half _Glossiness;
		half _Metallic;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
			UNITY_DEFINE_INSTANCED_PROP(int, _timeCount)
		UNITY_INSTANCING_BUFFER_END(Props)

		//vertex function
		void vert(inout appdata_custom v){
			UNITY_SETUP_INSTANCE_ID(v);
			//calculate uv coordinates
			int t = UNITY_ACCESS_INSTANCED_PROP(Props, _timeCount);
			float timeInFrames = (1.0f - t / (float)_numOfFrames);

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
		    nn.xy *= sqrt(max(l, 0.0f));
		    float3 f3 = nn.xyz * 2 + half3(0,0,-1);
			f3 = clamp(f3, -1.0, 1.0);
			f3.x *= -1;
			v.normal = normalize(f3);

			//set vertex colour
			fixed4 c = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
			v.color.rgb = c.rgb;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = IN.vcolor.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1.0f;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
