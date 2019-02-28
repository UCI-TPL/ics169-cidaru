// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/DistortUnlit"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DisplaceTex("Displacement Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0,0.1)) = 1
		_Color("Tint", Color) = (1,1,1,1)
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

				float4 frag(v2f i) : SV_Target
				{
					float2 distuv = float2(i.uv.x + _Time.x * 2, i.uv.y + _Time.x * 2);

					float2 disp = tex2D(_DisplaceTex, distuv).xy;
					disp = ((disp * 2) - 1) * _Magnitude;
					saturate(disp);
					float4 col = tex2D(_MainTex, i.uv + disp);
					return col;
				}
				ENDCG
        }
    }
}
