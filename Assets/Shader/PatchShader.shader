// Made with Amplify Shader Editor v1.9.0.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PatchShader"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_Lightmap("Lightmap", 2D) = "white" {}
		_EmissionStrength("EmissionStrength", Float) = 1
		_LightMapStrength("LightMapStrength", Range( 0 , 1)) = 0
		_TextureStrength("TextureStrength", Range( 0 , 1)) = 1
		_Highlight("Highlight", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float2 uv2_texcoord2;
		};

		uniform float _TextureStrength;
		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;
		uniform float _LightMapStrength;
		uniform sampler2D _Lightmap;
		uniform float4 _Lightmap_ST;
		uniform float _EmissionStrength;
		uniform float4 _Highlight;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST.xy + _MainTexture_ST.zw;
			float2 uv2_Lightmap = i.uv2_texcoord2 * _Lightmap_ST.xy + _Lightmap_ST.zw;
			float4 tex2DNode18 = tex2D( _Lightmap, uv2_Lightmap );
			float clampResult32 = clamp( ( tex2DNode18.a + ( 1.0 - _LightMapStrength ) ) , 0.0 , 1.0 );
			float4 MainTextureRef2 = ( ( tex2D( _MainTexture, uv_MainTexture ) - ( _LightMapStrength * tex2DNode18 ) ) * clampResult32 );
			o.Emission = ( _TextureStrength * ( ( MainTextureRef2 * _EmissionStrength ) * _Highlight ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19002
-1920;184;1920;893;3389.251;647.5386;1.6;True;False
Node;AmplifyShaderEditor.RangedFloatNode;30;-1713.933,273.1725;Inherit;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-2196.234,-33.62731;Inherit;False;Property;_LightMapStrength;LightMapStrength;6;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;29;-1620.333,416.1725;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-2461.595,171.6462;Inherit;True;Property;_Lightmap;Lightmap;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;1;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1808.834,27.47245;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-2274.545,-353.1061;Inherit;True;Property;_MainTexture;Main Texture;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-1573.534,195.1724;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;24;-1505.231,26.70164;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;32;-1426.634,179.5724;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1320.152,62.14651;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;2;-1089.863,-4.919876;Inherit;False;MainTextureRef;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-558.7595,-240.328;Inherit;False;Property;_EmissionStrength;EmissionStrength;2;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;4;-534.9876,-336.3946;Inherit;False;2;MainTextureRef;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;37;-248.5751,-162.9301;Inherit;False;Property;_Highlight;Highlight;8;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-285.6965,-289.907;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;21.82612,-377.4302;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-120.3742,-530.1304;Inherit;False;Property;_TextureStrength;TextureStrength;7;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OutlineNode;12;21.88148,186.2234;Inherit;False;0;True;None;1;0;Back;True;True;True;True;0;False;;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;8;-465.8916,90.72872;Inherit;False;Property;_OutlineColor;Outline Color;3;0;Create;True;0;0;0;False;0;False;1,0,0,1;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-198.5825,366.6984;Inherit;False;Property;_OutlineWidth;Outline Width;4;0;Create;True;0;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-246.2979,262.1898;Inherit;False;Property;_OpacityMaskOutline;OpacityMaskOutline;5;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;287.8248,-363.9305;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;412.1,-175.5;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;PatchShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;ForwardOnly;18;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;False;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;1;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;29;0;30;0
WireConnection;29;1;26;0
WireConnection;28;0;26;0
WireConnection;28;1;18;0
WireConnection;33;0;18;4
WireConnection;33;1;29;0
WireConnection;24;0;1;0
WireConnection;24;1;28;0
WireConnection;32;0;33;0
WireConnection;25;0;24;0
WireConnection;25;1;32;0
WireConnection;2;0;25;0
WireConnection;6;0;4;0
WireConnection;6;1;3;0
WireConnection;39;0;6;0
WireConnection;39;1;37;0
WireConnection;12;0;8;0
WireConnection;12;1;7;0
WireConnection;34;0;35;0
WireConnection;34;1;39;0
WireConnection;0;2;34;0
ASEEND*/
//CHKSM=5AE9FE9FE5CE446D35E68741FAE1B848394CBEC4