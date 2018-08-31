// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "gradient 2color"
{
	Properties
	{
		_Color1("Color 1", Color) = (1,0,0,0)
		_Color2("Color 2", Color) = (0,1,0.213793,0)
		_Distribution("Distribution", Range( 0 , 1)) = 0.2
		_StartPoint("Start Point", Range( -1 , 1)) = 0.1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float3 worldPos;
		};

		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform float _StartPoint;
		uniform float _Distribution;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 lerpResult7 = lerp( _Color1 , _Color2 , saturate( ( ( ( ase_vertex3Pos.y + _StartPoint ) + ( _Distribution / 2.0 ) ) / _Distribution ) ));
			o.Albedo = lerpResult7.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
7;29;889;1004;1478.473;688.6985;1.690818;True;False
Node;AmplifyShaderEditor.RangedFloatNode;14;-893.6871,131.5646;Float;False;Property;_Distribution;Distribution;2;0;Create;True;0;0;False;0;0.2;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;12;-892.5547,-125.1828;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-891.0083,39.46365;Float;False;Property;_StartPoint;Start Point;3;0;Create;True;0;0;False;0;0.1;-0.3;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-607.9615,-125.9681;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;18;-575.8928,-4.373093;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-415.9871,-126.3903;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;21;-447.9386,110.2116;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;22;-294.7735,-3.659806;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-511.2159,-349.1476;Float;False;Property;_Color2;Color 2;1;0;Create;True;0;0;False;0;0,1,0.213793,0;0.05406567,0.2390077,0.9191176,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-513.6301,-511.5334;Float;False;Property;_Color1;Color 1;0;0;Create;True;0;0;False;0;1,0,0,0;0.3211505,0.992647,0.9648609,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;7;-125.2009,-381.0247;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-322.7986,216.4767;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;20.38;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;26;-82.70605,57.31128;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;129.9141,-383.3432;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;gradient 2color;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;12;2
WireConnection;19;1;13;0
WireConnection;18;0;14;0
WireConnection;20;0;19;0
WireConnection;20;1;18;0
WireConnection;21;0;20;0
WireConnection;21;1;14;0
WireConnection;22;0;21;0
WireConnection;7;0;8;0
WireConnection;7;1;9;0
WireConnection;7;2;22;0
WireConnection;26;2;27;0
WireConnection;0;0;7;0
ASEEND*/
//CHKSM=74B731B19E8B6210C319AA29E51B84195A6341AD