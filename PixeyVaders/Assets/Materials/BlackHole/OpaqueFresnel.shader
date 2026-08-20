// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:7405,x:33095,y:32926,varname:node_7405,prsc:2|emission-398-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:3164,x:32132,y:33030,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:node_3164,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5805,x:32322,y:33030,varname:node_5805,prsc:2,ntxv:0,isnm:False|TEX-3164-TEX;n:type:ShaderForge.SFN_VertexColor,id:4876,x:32322,y:33163,varname:node_4876,prsc:2;n:type:ShaderForge.SFN_Fresnel,id:7656,x:32328,y:33407,varname:node_7656,prsc:2|EXP-2559-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2559,x:32338,y:33562,ptovrint:False,ptlb:FresnelValue,ptin:_FresnelValue,varname:node_2559,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:4633,x:32503,y:33407,varname:node_4633,prsc:2|VAL-7656-OUT,EXP-7129-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7129,x:32328,y:33345,ptovrint:False,ptlb:FresnelPower,ptin:_FresnelPower,varname:node_7129,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:398,x:32893,y:33027,varname:node_398,prsc:2|A-8749-RGB,B-2417-OUT;n:type:ShaderForge.SFN_Multiply,id:5059,x:32689,y:33407,varname:node_5059,prsc:2|A-4876-RGB,B-4633-OUT;n:type:ShaderForge.SFN_Color,id:8749,x:32534,y:32864,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8749,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0,c3:0.07843138,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:8096,x:32689,y:33327,ptovrint:False,ptlb:FresnelIntensity,ptin:_FresnelIntensity,varname:node_8096,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:2417,x:32689,y:33116,varname:node_2417,prsc:2|A-5059-OUT,B-8096-OUT;proporder:3164-2559-7129-8749-8096;pass:END;sub:END;*/

Shader "Custom/OpaqueFresnel" {
    Properties {
        _MainTexture ("Main Texture", 2D) = "white" {}
        _FresnelValue ("FresnelValue", Float ) = 1
        _FresnelPower ("FresnelPower", Float ) = 1
        _Color ("Color", Color) = (0.07843138,0,0.07843138,1)
        _FresnelIntensity ("FresnelIntensity", Float ) = 1.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelValue)
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelPower)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelIntensity)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float _FresnelValue_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelValue );
                float _FresnelPower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelPower );
                float _FresnelIntensity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelIntensity );
                float3 emissive = (_Color_var.rgb+((i.vertexColor.rgb*pow(pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelValue_var),_FresnelPower_var))*_FresnelIntensity_var));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
