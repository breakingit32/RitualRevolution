Shader "Custom/FogShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _FogColor("Fog Color", Color) = (1, 1, 1, 1)
        _FogAmount("Fog Amount", Range(0, 1)) = 0.5
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            LOD 100

            CGPROGRAM
            #pragma surface surf Lambert

            sampler2D _MainTex;
            float4 _FogColor;
            float _FogAmount;

            struct Input {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
                o.Alpha = _FogAmount;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
