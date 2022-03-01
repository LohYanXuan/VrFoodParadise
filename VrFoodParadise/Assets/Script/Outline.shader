Shader "Unlit/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor("Outline color", Color) = (0,0,0,1)
        _OutlineWidth("Outline width", Range(1.0,3.0)) = 1.01
    }

        CGINCLUDE
#include "UnityCG.cginc"

            struct appdata
        {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
        };

        struct v2f
        {
            float4 pos : POSITION;
            float3 normal : NORMAL;
        };

        float4 _OutlineColor;
        float _OutlineWidth;

        _OutlineColor("Outline color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline width", Range(1.0, 3.0)) = 1.01

        v2f vert(appdata v)
        {
            v.vertex.xyz *= _OutlineWidth;

            v2f o;
            o.pos = UnityOnjectToClipPos(v.vertex);
            return o;
        }

        ENDCG

        SubShader
        {
            Pass //Render the Outline
            {
                ZWrite Off

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                half4 frag(v2f 1) : COLOR
                {
                return _OutlineColor;
                }
            }
            ENDCG
        }

        Pass //Normal render
        {
            ZWrite On

            Material
        }
    }
}
