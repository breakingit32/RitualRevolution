Shader "Custom/HexagonWaveShader" {
    Properties{
        _MainTex("Hexagon Texture", 2D) = "white" {}
        _Speed("Speed", Range(0, 10)) = 1
        _Amplitude("Amplitude", Range(0, 1)) = 0.1
        _Frequency("Frequency", Range(0, 10)) = 1
        _Tiling("Tiling", Range(1, 10)) = 1
    }

        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Speed;
            float _Amplitude;
            float _Frequency;
            float _Tiling;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float wave = _Amplitude * sin((_Frequency * v.uv.x * 2.0 * 3.14159) + (_Speed * _Time.y));
                v.uv.y += wave;
                v.uv = v.uv * _Tiling;
                o.uv = v.uv;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
