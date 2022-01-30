Shader "Game/FinalCameraRender"
{
    Properties
    {
        _CameraOrigin("Camera Origin", 2D) = "white" {}
        _CameraTarget("Camera Target", 2D) = "white" {}

        _Mix1("Mix Values 1", 2D) = "white" {}
        _Layout1("Layout Values 1", 2D) = "white" {}
        _Mix2("Mix Values 2", 2D) = "white" {}
        _Layout2("Layout Values 2", 2D) = "white" {}

        _LayoutMixValue("Layout Mix Value", float) = 0
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

            float eerp(float a, float b, float t)
            {
                return pow(a, 1 - t) * pow(b, t);
            }

            sampler2D _CameraOrigin;
            sampler2D _CameraTarget;
            sampler2D _Mix1;
            sampler2D _Layout1;
            sampler2D _Mix2;
            sampler2D _Layout2;
            float _LayoutMixValue;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 camOri = tex2D(_CameraOrigin, i.uv);
                fixed4 camTarget = tex2D(_CameraTarget, i.uv);


                float mixVal1 = 1 - tex2D(_Mix1, i.uv).r;
                float mixVal2 = 1 - tex2D(_Mix2, i.uv).r;

                float mixVal = lerp(mixVal1, mixVal2, _LayoutMixValue);

                fixed4 color = lerp(camOri, camTarget, mixVal);

                fixed4 layout1 = tex2D(_Layout1, i.uv);
                fixed4 layout2 = tex2D(_Layout2, i.uv);


                fixed4 layout = fixed4(lerp(layout1.rgb, layout2.rgb, _LayoutMixValue), lerp(layout1.a, layout2.a, _LayoutMixValue));

                return lerp(color, layout, layout.a);
            }
            ENDCG
        }
    }
}
