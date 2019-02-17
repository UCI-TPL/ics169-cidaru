Shader "Unlit/Death"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Texture",2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_DissolveSpeed("Dissolve Speed", float) = 1.0
		_DissolveColor1("Dissolve Color 1", Color) = (1, 1, 1, 1)
		_ColorThreshold1("Color Threshold 1", float) = 1.0
		_StartTime("Start Time", float) = 1.0
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

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// Properties
			float4 _Color;
			float4 _DissolveColor1;
			sampler2D _MainTex;
			sampler2D _NoiseTex;
			float _DissolveSpeed;
			float _ColorThreshold1;
			float _StartTime;

			float _MainTex_TexelSize;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			vertexOutput vert(appdata input)
			{
				vertexOutput output;

				// convert to world space
				output.pos = UnityObjectToClipPos(input.vertex);

				// texture coordinates 
				output.uv = input.uv;

				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{

				// sample noise texture
				float4 noiseSample = tex2Dlod(_NoiseTex, float4(input.uv.xy, 0, 0));
				
				//get main texture/color
				float4 col = tex2D(_MainTex, input.uv);
				
				// edge color				
				float thresh1 = _Time * _ColorThreshold1 - _StartTime;
				float useDissolve1 = noiseSample - thresh1 < 0;
				col.rgb = (1 - useDissolve1)*col.rgb + useDissolve1 * _DissolveColor1;
				
			
				float threshold = _Time *_DissolveSpeed - _StartTime;
				clip(noiseSample - threshold);

				
				return col;
			}

			ENDCG
		}
	}
}
