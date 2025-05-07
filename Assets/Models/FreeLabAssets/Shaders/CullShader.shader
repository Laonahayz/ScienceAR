Shader "Unlit/CullShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		Value("ClipValue",Range(-1,100)) = 0
			   _Color("Color", Color) = (1,1,1,1)
	}

		CGINCLUDE
#include "UnityCG.cginc"
#include "Lighting.cginc"

			struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			float3 normal:NORMAL;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
			float4 pos:TEXCOORD1;
			float3 WorldNor:TEXCOORD2;
			float4 WorldPos:TEXCOORD3;
		};
		fixed4 _Color;
		sampler2D _MainTex;
		float4 _MainTex_ST;
		float Value;
		uniform float objPosY;

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.WorldPos = mul(UNITY_MATRIX_M, v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			o.WorldNor = normalize(UnityObjectToWorldNormal(v.normal));
			o.pos = v.vertex;
			return o;
		}

		fixed4 fragfront(v2f i) : SV_Target
		{
			if (i.WorldPos.y > Value + objPosY) {
				discard;
			}
			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
			float3 LightDir = normalize(_WorldSpaceLightPos0.xyz);
			fixed4 col = _Color;
			float3 diffuse = _LightColor0 * col * max(0, dot(i.WorldNor,LightDir) * 0.5 + 0.5);
			fixed3 color = ambient + diffuse;
			return fixed4(color, _Color.a);
		}
			fixed4 fragback(v2f i) : SV_Target
		{
			if (i.WorldPos.y > Value + objPosY) {
				discard;
			}
			 return _Color;
		}
			ENDCG
			SubShader
		{
			Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
				ZWrite On
				Blend SrcAlpha OneMinusSrcAlpha
				LOD 100
				Pass
			{

				cull back//剔除背面，渲染正面
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment fragfront       
				ENDCG
			}
				Pass
			{
			   cull front//剔除正面，渲染背面
			   CGPROGRAM
			   #pragma vertex vert
			   #pragma fragment fragback       
			   ENDCG
			}
		}
}