// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/SHD_WaterStream"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_GlitchTex("Glitch Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent"
				"Queue" = "Transparent"
		}
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _GlitchTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
			float randomOffset = unity_ObjectToWorld[0].w*2 + unity_ObjectToWorld[0].x;

            // sample the texture
            float2 pannedMainCoord = float2(
				i.uv.x + cos(i.uv.y * 10) * 0.08,
				frac(i.uv.y + _Time.y * 0.4 + randomOffset * 1.2)
            );
            fixed4 mainCol = tex2D(_MainTex, pannedMainCoord);
            float2 pannedMainCoord2 = float2(
				i.uv.x,
				frac(i.uv.y + _Time.y * 0.2 + randomOffset * 1.4)
            );
            fixed4 mainCol2 = tex2D(_MainTex, pannedMainCoord2);

			// glitch 1
			float2 pannedTexCoord = float2(
				i.uv.x + cos(i.uv.y * 10) * 0.08, // add a cosinus from y to create wavy effet
				frac(i.uv.y + _Time.y * 0.5 + randomOffset * 1.5)
				);
			fixed4 glitchCol = tex2D(_GlitchTex, pannedTexCoord);
            float2 pannedTexCoord2 = float2(
				i.uv.x,
				frac(i.uv.y + _Time.y * 0.8 +  + randomOffset * 1.8)
				);
			fixed4 glitchCol2 = tex2D(_GlitchTex, pannedTexCoord2);
			/*
			float col.a = mainCol.a * glitchCol.r;
			*/

            float x = i.uv.y * 2 - 1;
            float fadeInOut = 1 - x * x * x * x; // this makes an alpha transition when entering and existing
			mainCol.a = clamp(mainCol.a * glitchCol.r + mainCol2.a * glitchCol2.r, 0, 1) * fadeInOut;
            mainCol.rgb = max(mainCol.rgb, mainCol2.rgb);

			// apply fog
			UNITY_APPLY_FOG(i.fogCoord, col);
			return mainCol;
		}
		ENDCG
	}
	}
}
