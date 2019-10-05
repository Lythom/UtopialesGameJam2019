﻿Shader "Unlit/SHD_WaterStream"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_GlitchTex("Glitch Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" 
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                // sample the texture
                fixed4 mainCol = tex2D(_MainTex, i.uv);

			// glitch 1
			float2 pannedTexCoord = float2(
				i.uv.x,
				frac(i.uv.y + _Time.y * 0.0332)
				);
			fixed4 glitchCol = tex2D(_GlitchTex, pannedTexCoord);
			/*
			float col.a = mainCol.a * glitchCol.r;
			*/

			
			mainCol.a = mainCol.a *  glitchCol.r;
			

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return mainCol;
            }
            ENDCG
        }
    }
}
