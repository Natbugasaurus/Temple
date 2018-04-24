// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Solid"
{
	Properties
	{

		_MainTex ("Main Texture", 2D) = "white" {}
		_Color("Color", Color) = (0, 0, 0, 0)
		_ShadeColor("Shade Color", Color) = (0, 0, 0, 0)
		_LightColor("Light Color", Color) = (0,0,0,0)
		_DarkestDot("Darkest DotP", Float) = 0
		_MidDot("Mid DotP", Float) = 0
		
		
		

	}
	SubShader{
		
		Pass{

		
		
		Tags{ "LightMode" = "ForwardBase" "RenderType" = "Opaque" }
		
		Cull Off

		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fwdbase
		#include "UnityCG.cginc"
		#include "AutoLight.cginc"
		

		struct appdata
		{
			half4 vertex : POSITION;
			half2 uv: TEXCOORD0;
			half3  normal  : NORMAL;
			

		};

		struct v2f
		{
			half4 pos : SV_POSITION;
			
			half2 uv : TEXCOORD5;
			
			LIGHTING_COORDS(1, 2)

			half3  lightDir  : TEXCOORD3;
			half3  normal  : TEXCOORD4;

			half4 lCol : TEXCOORD6;
		};

		
		sampler2D _MainTex;
		fixed4 _MainTex_ST;
		fixed4 _Color;
		fixed4 _ShadeColor;
		fixed4 _LightColor;

		float _DarkestDot;
		float _MidDot;
		
		half4 _LightColor0;

		v2f vert(appdata v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.normal =  UnityObjectToWorldNormal(v.normal);
			o.uv = v.uv;
			o.lightDir = WorldSpaceLightDir(v.vertex);

			TRANSFER_VERTEX_TO_FRAGMENT(o);
			
			
			return o;
		}

		

		fixed4 frag(v2f i) : SV_Target {

			fixed attenuation = LIGHT_ATTENUATION(i);
		
			fixed4 tex = tex2D(_MainTex, i.uv.xy);

			half4 col;
			col = tex * _Color;

			if(attenuation < 0.01 || dot(i.normal, i.lightDir) < -0.2){
				col = col * _ShadeColor * 0.6;
			}else{
				if(dot(i.normal, i.lightDir) < _DarkestDot){
					col = col * (_ShadeColor * 0.8);
				}else if(dot(i.normal, i.lightDir) < _MidDot){
					col =  col * (_ShadeColor );
				}
			}

			if(dot(i.normal, i.lightDir) > 0.8){
				col = col + (_LightColor0*0.1);
			}

			return col;
		}

			ENDCG
	}

		

		
	Pass 
         {
             Name "ShadowCaster"
             Tags { "LightMode" = "ShadowCaster" }
             
             Fog {Mode Off}
             ZWrite On ZTest LEqual Cull Off
             Offset 1, 1
 
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_shadowcaster
             #include "UnityCG.cginc"
 
             struct v2f 
             { 
                 V2F_SHADOW_CASTER;
             };
 
             v2f vert( appdata_base v )
             {
                 v2f o;
                 TRANSFER_SHADOW_CASTER(o)
                 return o;
             }
 
             float4 frag( v2f i ) : SV_Target
             {
                 SHADOW_CASTER_FRAGMENT(i)
             }
             ENDCG
 
         }


	}

	Fallback "VertexLit"
}
