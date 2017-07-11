// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ARIndicatorShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_DistPoint("CurrentDistance", float) = 0.0
		_DistAnchor("CurrentAnchor", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform float4 _Color;
			uniform float3 _DistAnchor;
			uniform float _DistPoint;

			struct Input {
				float4 pos: POSITION;
			};

			struct FragmentInput {
				float4 pos: SV_POSITION;
				float dist: TEXCOORD0;
			};

			FragmentInput vert(Input v){
				FragmentInput o;
				o.pos = UnityObjectToClipPos(v.pos);
				o.dist = length(mul(unity_ObjectToWorld,v.pos).xz - _DistAnchor.xz);
				return o;
			}

			half4 frag(FragmentInput f) : COLOR{
				//float a = 0.0f;
				//if(f.dist < _DistPoint){
					float a = abs( 0.03f * f.dist /  ( f.dist - _DistPoint));
					//float a = f.dist;
				//}
				return float4(_Color.rgb, a);
			}

			ENDCG
		}
	}
	Fallback off
}
