Shader "Unlit/Distort"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_NoiseTexture("noise texture",2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"  "Queue" = "Transparent"}
        LOD 100
		Blend SrcAlpha OneMinusSrcAlpha
		Cull off
		ZWrite Off
		GrabPass
		{
		"_BackgroundTexture"
		}
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

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

            sampler2D _MainTex, _NoiseTexture;
			sampler2D _BackgroundTexture;

            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				float2 distort = i.uv;
				float4 noise = tex2D(_NoiseTexture, i.uv+_Time);
				distort.x = (i.uv.x+sin(noise.x));
				distort.y =(i.uv.y+sin(noise.y));
			

                fixed4 col = tex2D(_BackgroundTexture,distort);
                // apply fog
                return col;
            }
            ENDCG
        }
    }
}
