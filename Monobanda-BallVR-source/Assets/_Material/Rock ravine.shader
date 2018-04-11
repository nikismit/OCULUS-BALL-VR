// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:2,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.009709475,fgcg:0.003027681,fgcb:0.05147058,fgca:0.4117647,fgde:1,fgrn:12,fgrf:350,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2440,x:34702,y:32470,varname:node_2440,prsc:2|diff-5866-OUT,diffpow-5866-OUT,gloss-3763-OUT,lwrap-5336-OUT,amdfl-5336-OUT;n:type:ShaderForge.SFN_Color,id:3941,x:33334,y:32316,ptovrint:False,ptlb:base color,ptin:_basecolor,varname:node_3941,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.251211,c2:0.2024221,c3:0.3529412,c4:1;n:type:ShaderForge.SFN_Fresnel,id:2432,x:33430,y:32715,varname:node_2432,prsc:2|NRM-9157-OUT,EXP-4880-OUT;n:type:ShaderForge.SFN_NormalVector,id:9157,x:33152,y:32651,prsc:2,pt:True;n:type:ShaderForge.SFN_Add,id:5866,x:34125,y:32474,varname:node_5866,prsc:2|A-3941-RGB,B-5471-OUT;n:type:ShaderForge.SFN_Vector1,id:4880,x:33258,y:32864,varname:node_4880,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:2418,x:33334,y:32501,ptovrint:False,ptlb:node_2418,ptin:_node_2418,varname:node_2418,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4000865,c2:0.6798114,c3:0.7352941,c4:1;n:type:ShaderForge.SFN_Multiply,id:5471,x:33591,y:32539,varname:node_5471,prsc:2|A-2418-RGB,B-2432-OUT;n:type:ShaderForge.SFN_Vector1,id:3763,x:33739,y:32347,varname:node_3763,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Color,id:3563,x:34059,y:32718,ptovrint:False,ptlb:base color_copy,ptin:_basecolor_copy,varname:_basecolor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5511157,c2:0,c3:0.5588235,c4:1;n:type:ShaderForge.SFN_Fresnel,id:3750,x:34111,y:33094,varname:node_3750,prsc:2|NRM-2687-OUT,EXP-6074-OUT;n:type:ShaderForge.SFN_NormalVector,id:2687,x:33898,y:33094,prsc:2,pt:True;n:type:ShaderForge.SFN_Add,id:5336,x:34359,y:32656,varname:node_5336,prsc:2|A-3563-RGB,B-8098-OUT;n:type:ShaderForge.SFN_Vector1,id:6074,x:33819,y:33226,varname:node_6074,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:8098,x:34257,y:32887,varname:node_8098,prsc:2|A-6633-RGB,B-3750-OUT;n:type:ShaderForge.SFN_Color,id:6633,x:33938,y:32907,ptovrint:False,ptlb:node_9448_copy,ptin:_node_9448_copy,varname:_node_9448_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.9586205,c4:1;proporder:3941-2418-3563-6633;pass:END;sub:END;*/

Shader "Rock_ravine" {
    Properties {
        _basecolor ("base color", Color) = (0.251211,0.2024221,0.3529412,1)
        _node_2418 ("node_2418", Color) = (0.4000865,0.6798114,0.7352941,1)
        _basecolor_copy ("base color_copy", Color) = (0.5511157,0,0.5588235,1)
        _node_9448_copy ("node_9448_copy", Color) = (0,1,0.9586205,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _basecolor;
            uniform float4 _node_2418;
            uniform float4 _basecolor_copy;
            uniform float4 _node_9448_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 node_5336 = (_basecolor_copy.rgb+(_node_9448_copy.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)));
                float3 w = node_5336*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 node_5866 = (_basecolor.rgb+(_node_2418.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)));
                float3 forwardLight = pow(max(float3(0.0,0.0,0.0), NdotLWrap + w ), node_5866);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = forwardLight * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += node_5336; // Diffuse Ambient Light
                float3 diffuseColor = node_5866;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _basecolor;
            uniform float4 _node_2418;
            uniform float4 _basecolor_copy;
            uniform float4 _node_9448_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 node_5336 = (_basecolor_copy.rgb+(_node_9448_copy.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)));
                float3 w = node_5336*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 node_5866 = (_basecolor.rgb+(_node_2418.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)));
                float3 forwardLight = pow(max(float3(0.0,0.0,0.0), NdotLWrap + w ), node_5866);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = forwardLight * attenColor;
                float3 diffuseColor = node_5866;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
