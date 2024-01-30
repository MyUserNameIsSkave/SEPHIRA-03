Shader "Custom/VertAnimShaderNewSurfaceShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PosTex("Position Texture", 2D) = "black" {}
        _NormalTex("Normal Texture", 2D) = "white" {}
        _Length("Animation Length", float) = 1
        _DT("Delta Time", float) = 0
    }


    SubShader
    {
        Pass{







            
            CGPROGRAM

            //enable GPU instancing support
            #pragma multi_compile_instancing
            #pragma instancing_options renderinglayer
            #pragma multi_compile _ DOTS_INSTANCING_ON




            //#pragma multi_compile_instancing
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #define TS _PosTex_TexelSize

            struct appdata {
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 position : SV_POSITION;
            };

            sampler2D _MainTex, _PosTex, _NormalTex;
            float4 _PosTex_TexelSize;
            float _Length, _DT;

            v2f vert(appdata v, uint vid : SV_VertexID)
            {
                float t = (_Time.y - _DT / _Length);
                t = fmod(t, 1.0);
                float x = (vid + 0.5) * TS.x;
                float y = t;
                float4 pos = tex2Dlod(_PosTex, float4(x, y, 0, 0));
                float3 normal = tex2Dlod(_NormalTex, float4(x, y, 0, 0)).rgb;

                v2f o;
                o.position = UnityObjectToClipPos(pos);
                o.normal = UnityObjectToWorldNormal(normal);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_TARGET
            {
                half4 diff = dot(i.normal, float3(0, 1, 0)) * 0.5 + 0.5;
                half4 col = tex2D(_MainTex, i.uv);
                return diff * col;
            }
            ENDCG
        }
    }
}

