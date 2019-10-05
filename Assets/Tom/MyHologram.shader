// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/MyHologram"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _GlitchTex ("Glitch Texture", 2D) = "white" {}    
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha One

        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag2
            #include "UnitySprites.cginc"

            sampler2D _GlitchTex;

            fixed4 SpriteFrag2(v2f i) : SV_Target
            {
                fixed4 mainCol = tex2D(_MainTex, i.texcoord);

                // glitch 1
                float2 pannedTexCoord = float2(
                    i.texcoord.x,
                    frac(i.texcoord.y + _Time.y * 0.0332)
                );
                fixed4 glitchCol = tex2D(_GlitchTex, pannedTexCoord);
               
                fixed4 col = (mainCol * 0.6 + mainCol * glitchCol);

                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}