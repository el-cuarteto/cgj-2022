Shader "Game/FinalCameraRender"
{
    Properties
    {
        _CameraOrigin("Camera Origin", 2D) = "white" {}
        _CameraTarget("Camera Target", 2D) = "white" {}
        _Mix("Mix Values", 2D) = "white" {}
        _Layout("Layout Values", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _CameraOrigin;
            sampler2D _CameraTarget;
            sampler2D _Mix;
            sampler2D _Layout;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 camOri = tex2D(_CameraOrigin, i.uv);
                fixed4 camTarget = tex2D(_CameraTarget, i.uv);
                float mixVal = 1 - tex2D(_Mix, i.uv).r;

                fixed4 color = lerp(camOri, camTarget, mixVal);

                fixed4 layout = tex2D(_Layout, i.uv);


                return lerp(color, layout, layout.a);
            }
            ENDCG
        }
    }
}
