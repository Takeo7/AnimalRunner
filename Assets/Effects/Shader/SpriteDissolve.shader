// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:True,atwp:True,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:33632,y:32796,varname:node_1873,prsc:2|emission-7848-OUT,alpha-603-OUT,clip-8492-OUT;n:type:ShaderForge.SFN_Tex2d,id:4805,x:32485,y:32430,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1086,x:32827,y:32642,cmnt:RGB,varname:node_1086,prsc:2|A-4805-RGB,B-5983-RGB,C-5376-RGB;n:type:ShaderForge.SFN_Color,id:5983,x:32485,y:32658,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5376,x:32485,y:32835,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1749,x:33083,y:32780,cmnt:Premultiply Alpha,varname:node_1749,prsc:2|A-1086-OUT,B-603-OUT;n:type:ShaderForge.SFN_Multiply,id:603,x:32899,y:32898,cmnt:A,varname:node_603,prsc:2|A-4805-A,B-5983-A,C-5376-A;n:type:ShaderForge.SFN_Slider,id:2551,x:31884,y:33411,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:node_2551,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6239316,max:1;n:type:ShaderForge.SFN_RemapRange,id:8181,x:32468,y:33383,varname:node_8181,prsc:2,frmn:0,frmx:1,tomn:1,tomx:-1|IN-1560-OUT;n:type:ShaderForge.SFN_Tex2d,id:9933,x:32454,y:33220,ptovrint:False,ptlb:DissolveGuide,ptin:_DissolveGuide,varname:node_9933,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:2042,x:32675,y:33284,varname:node_2042,prsc:2|A-9933-R,B-8181-OUT;n:type:ShaderForge.SFN_Clamp01,id:8492,x:32861,y:33284,varname:node_8492,prsc:2|IN-2042-OUT;n:type:ShaderForge.SFN_RemapRange,id:6606,x:32675,y:33444,varname:node_6606,prsc:2,frmn:0,frmx:1,tomn:-4,tomx:4|IN-2042-OUT;n:type:ShaderForge.SFN_OneMinus,id:1560,x:32298,y:33383,varname:node_1560,prsc:2|IN-2551-OUT;n:type:ShaderForge.SFN_Clamp01,id:8108,x:32861,y:33444,varname:node_8108,prsc:2|IN-6606-OUT;n:type:ShaderForge.SFN_OneMinus,id:8988,x:33034,y:33444,varname:node_8988,prsc:2|IN-8108-OUT;n:type:ShaderForge.SFN_Color,id:1320,x:32899,y:33040,ptovrint:False,ptlb:BorderColor,ptin:_BorderColor,varname:node_1320,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:7848,x:33338,y:32880,varname:node_7848,prsc:2|A-1749-OUT,B-1320-RGB,T-6629-OUT;n:type:ShaderForge.SFN_Multiply,id:7071,x:33250,y:33353,varname:node_7071,prsc:2|A-603-OUT,B-8988-OUT;n:type:ShaderForge.SFN_Clamp01,id:6629,x:33250,y:33227,varname:node_6629,prsc:2|IN-7071-OUT;proporder:2551-5983-4805-9933-1320;pass:END;sub:END;*/

Shader "VaxKun/SpriteDissolve" {
    Properties {
        _Dissolve ("Dissolve", Range(0, 1)) = 0.6239316
        _Color ("Color", Color) = (1,1,1,1)
        [PerRendererData]_MainTex ("MainTex", 2D) = "white" {}
        _DissolveGuide ("DissolveGuide", 2D) = "white" {}
        _BorderColor ("BorderColor", Color) = (1,1,1,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _Stencil ("Stencil ID", Float) = 0
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilOpFail ("Stencil Fail Operation", Float) = 0
        _StencilOpZFail ("Stencil Z-Fail Operation", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            Stencil {
                Ref [_Stencil]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
                Comp [_StencilComp]
                Pass [_StencilOp]
                Fail [_StencilOpFail]
                ZFail [_StencilOpZFail]
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float _Dissolve;
            uniform sampler2D _DissolveGuide; uniform float4 _DissolveGuide_ST;
            uniform float4 _BorderColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _DissolveGuide_var = tex2D(_DissolveGuide,TRANSFORM_TEX(i.uv0, _DissolveGuide));
                float node_2042 = (_DissolveGuide_var.r+((1.0 - _Dissolve)*-2.0+1.0));
                clip(saturate(node_2042) - 0.5);
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_1086 = (_MainTex_var.rgb*_Color.rgb*i.vertexColor.rgb); // RGB
                float node_603 = (_MainTex_var.a*_Color.a*i.vertexColor.a); // A
                float3 node_1749 = (node_1086*node_603); // Premultiply Alpha
                float node_8988 = (1.0 - saturate((node_2042*8.0+-4.0)));
                float3 node_7848 = lerp(node_1749,_BorderColor.rgb,saturate((node_603*node_8988)));
                float3 emissive = node_7848;
                float3 finalColor = emissive;
                return fixed4(finalColor,node_603);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Dissolve;
            uniform sampler2D _DissolveGuide; uniform float4 _DissolveGuide_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _DissolveGuide_var = tex2D(_DissolveGuide,TRANSFORM_TEX(i.uv0, _DissolveGuide));
                float node_2042 = (_DissolveGuide_var.r+((1.0 - _Dissolve)*-2.0+1.0));
                clip(saturate(node_2042) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
