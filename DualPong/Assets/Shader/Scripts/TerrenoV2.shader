Shader "Unlit/TerrenoV2"
{
    Properties
    {
        _MainTexA("Texture A", 2D) = "white" {}
        _MainTexB("Texture B", 2D) = "white" {}
        _MainTexAlpha("Texture Alpha", 2D) = "white" {}
        _TimeOffset("Time Offset", Float) = 0.0
    }
    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;//vertex transformado
            };

            sampler2D _MainTexA;
            sampler2D _MainTexB;
            sampler2D _MainTexAlpha;
            float _TimeOffset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 colA = tex2D(_MainTexA, i.uv);
                fixed4 colB = tex2D(_MainTexB, i.uv);
                
                float mul = _TimeOffset;
                /*if (_Time.y < 1.0) {
                    mul = _Time.y;
                }*/

                fixed4 valAlpha = tex2D(_MainTexAlpha, i.uv * mul);

                float4 col = (valAlpha * colA) + ((1.0-valAlpha) * colB);

                return col;
            }
            ENDCG
        }
    }
}
