Shader "fruitshader/lime1"
{
	Properties 
	{
_BaseColor("_BaseColor", Color) = (1,1,1,1)
_BaseTexture("_BaseTexture", 2D) = "white" {}
_Brightness("_Brightness", Range(0,2) ) = 0.5
_EmissiveMult("_EmissiveMult", Float) = 1
_FaceTexture("_FaceTexture", 2D) = "black" {}
_Alpha("_Alpha", 2D) = "white" {}
_RimPower("_RimPower", Range(4,0.3) ) = 4
_RimMult("_RimMult", Float) = 1
_Specularity("_Specularity", Range(0,2) ) = 0
_Glossiness("_Glossiness", Range(0,1) ) = 0
_outlineThickness("_outlineThickness", Range(1,0) ) = 0.5
_threshold("_threshold", Range(0,10) ) = 0.5
_brightnessB("_brightnessB", Range(0,1) ) = 0.5

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Off
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  noambient nolightmap vertex:vert
#pragma target 2.0


float4 _BaseColor;
sampler2D _BaseTexture;
float _Brightness;
float _EmissiveMult;
sampler2D _FaceTexture;
sampler2D _Alpha;
float _RimPower;
float _RimMult;
float _Specularity;
float _Glossiness;
float _outlineThickness;
float _threshold;
float _brightnessB;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}

			inline half4 LightingBlinnPhongEditor_DirLightmap (EditorSurfaceOutput s, fixed4 color, fixed4 scale, half3 viewDir, bool surfFuncWritesNormal, out half3 specColor)
			{
				UNITY_DIRBASIS
				half3 scalePerBasisVector;
				
				half3 lm = DirLightmapDiffuse (unity_DirBasis, color, scale, s.Normal, surfFuncWritesNormal, scalePerBasisVector);
				
				half3 lightDir = normalize (scalePerBasisVector.x * unity_DirBasis[0] + scalePerBasisVector.y * unity_DirBasis[1] + scalePerBasisVector.z * unity_DirBasis[2]);
				half3 h = normalize (lightDir + viewDir);
			
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular * 128.0);
				
				// specColor used outside in the forward path, compiled out in prepass
				specColor = lm * _SpecColor.rgb * s.Gloss * spec;
				
				// spec from the alpha component is used to calculate specular
				// in the Lighting*_Prepass function, it's not used in forward
				return half4(lm, spec);
			}
			
			struct Input {
				float4 color : COLOR;
float3 viewDir;
float2 uv_BaseTexture;
float4 meshUV;
float2 uv_Alpha;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);

o.meshUV.xy = v.texcoord.xy;
o.meshUV.zw = v.texcoord1.xy;

			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Fresnel0_1_NoInput = float4(0,0,1,1);
float4 Fresnel0=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( Fresnel0_1_NoInput.xyz ) )).xxxx;
float4 Invert0= float4(1.0, 1.0, 1.0, 1.0) - Fresnel0;
float4 Add0=Invert0 + _outlineThickness.xxxx;
float4 Min0=min(Add0,float4( 1.0, 1.0, 1.0, 1.0 ));
float4 Min1=min(Min0,_brightnessB.xxxx);
float4 Log0=log(Min1);
float4 Max0=max(_brightnessB.xxxx,Min0);
float4 Pow1=pow(Max0,_threshold.xxxx);
float4 Add2=Log0 + Pow1;
float4 Tex2D0=tex2D(_BaseTexture,(IN.uv_BaseTexture.xyxy).xy);
float4 Multiply6=_BaseColor * Tex2D0;
float4 Multiply13=Multiply6 * _EmissiveMult.xxxx;
float4 Pow0=pow(Fresnel0,_RimPower.xxxx);
float4 Multiply3=Multiply6 * Pow0;
float4 Multiply14=Multiply3 * _RimMult.xxxx;
float4 Lerp3=lerp(Multiply13,Multiply14,Multiply14);
float4 Multiply1=_Brightness.xxxx * Multiply6;
float4 Add1=Lerp3 + Multiply1;
float4 Tex2D2=tex2D(_FaceTexture,(IN.meshUV.zwzw).xy);
float4 Lerp0=lerp(Add1,Tex2D2,Tex2D2.aaaa);
float4 Multiply0=Add2 * Lerp0;
float4 Multiply2=IN.color * Multiply0;
float4 Multiply4=IN.color * _Specularity.xxxx;
float4 Invert1= float4(1.0, 1.0, 1.0, 1.0) - Tex2D2.aaaa;
float4 Multiply5=Multiply4 * Invert1;
float4 Tex2D1=tex2D(_Alpha,(IN.uv_Alpha.xyxy).xy);
float4 Master0_0_NoInput = float4(0,0,0,0);
float4 Master0_1_NoInput = float4(0,0,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Emission = Multiply2;
o.Specular = _Glossiness.xxxx;
o.Gloss = Multiply5;
o.Alpha = Tex2D1;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}