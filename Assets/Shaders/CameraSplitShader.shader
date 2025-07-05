Shader "Custom/CameraSplitShader"
{
    Properties
    {
        _TexA("Texture A", 2D) = "white" {}
        _TexB("Texture B", 2D) = "white" {}
        
        _SplitAngle("Split Angle", Range(0, 180)) = 45
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _TexA;
            sampler2D _TexB;

            float _SplitAngle;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float2 center = float2(0.5, 0.5);
                float2 offset = uv - center;

                float angleRad = radians(_SplitAngle);
                float2x2 rot = float2x2(cos(angleRad), -sin(angleRad), sin(angleRad), cos(angleRad));
                float2 rotated = mul(rot, offset);

                float edge = 0.02;
                float t = smoothstep(-edge, edge, rotated.y);

                return lerp(tex2D(_TexB, uv), tex2D(_TexA, uv), t);
            }
            ENDCG
        }
    }
}
