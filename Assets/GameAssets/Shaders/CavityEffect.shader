Shader "Custom/CavityEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CavityStrength ("Cavity Strength", Range(0, 1)) = 0.5
        _RidgeStrength ("Ridge Strength", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        float _CavityStrength;
        float _RidgeStrength;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 worldPos;
        };

        half4 LightingStandard(SurfaceOutputStandard s, half3 lightDir, half atten)
        {
            half4 c;
            half NdotL = dot(s.Normal, lightDir);
            half3 diff = NdotL * _LightColor0;
            half3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * s.Albedo;
            c.rgb = (diff + ambient) * s.Albedo;
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half4 tex = tex2D(_MainTex, IN.uv_MainTex);
            float cavity = 1.0 - dot(IN.worldNormal, float3(0, 1, 0));
            cavity = pow(cavity, _CavityStrength);
            float ridge = dot(IN.worldNormal, float3(0, 1, 0));
            ridge = pow(ridge, _RidgeStrength);
            o.Albedo = tex.rgb * (1 - cavity) + ridge;
            o.Alpha = tex.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
