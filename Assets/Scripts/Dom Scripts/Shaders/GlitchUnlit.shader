Shader "Unlit/GlitchUnlit"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DisplaceTex("Displacement Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0,0.1)) = 1
		_Color("Tint", Color) = (1,1,1,1)
		_Cutoff("Cutoff", Range(0,1)) = 0
		_Fade("Fade", Range(0, 1)) = 0
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}
		SubShader
		{
			Tags
			{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
			}
		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

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

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;

					return o;
				}

				sampler2D _MainTex;
				sampler2D _DisplaceTex;
				float _Magnitude;
				float _Cutoff;
				float4 _Color;
				float _Fade;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 transit = tex2D(_DisplaceTex, i.uv);

					fixed2 direction = float2(0,0);
					direction = normalize(float2((transit.r - 0.5) * 2, (transit.g - 0.5) * 2));

					fixed4 col = tex2D(_MainTex, i.uv + _Cutoff * direction);

					if (transit.b < _Cutoff)
						return col = lerp(col, _Color, _Fade);

					return col;
				}
				ENDCG
        }
    }
}
