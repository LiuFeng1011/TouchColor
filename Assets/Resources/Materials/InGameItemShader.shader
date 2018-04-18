Shader "Custom/InGameItemShader" {
	Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Float) = 10
    }

    CGINCLUDE 

        #include "Lighting.cginc"  
        #include "unitycg.cginc"   

        fixed4 _Color;
        float _Shininess;

        struct appdata
        {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;    
            float3 normal : NORMAL;  
        };
        struct v2f
        {
            float4 vertex : SV_POSITION;
            float3 posWorld : TEXCOORD0;  
            float3 normalDir : TEXCOORD1;
            float4 vertex2 : TEXCOORD2;
        };

        v2f vert (appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.vertex2 = v.vertex;
            o.posWorld = mul(unity_ObjectToWorld,v.vertex).xyz;  
            o.normalDir = normalize(UnityObjectToWorldNormal(v.normal)); 

            return o;
        }

        float4 getcolor(v2f i){
            
            float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld);

            //获取法线方向  
            fixed3 worldNormal = i.normalDir;  
            //灯光方向  
            fixed3 worldLightDir = normalize(_WorldSpaceLightPos0).xyz;   

            float r = dot(viewDirection,worldNormal) - dot(worldLightDir,worldNormal);
            return _Color * (1-r);

            /*
            fixed rate = dot(reflect(-worldLightDir, worldNormal),viewDirection);
            float3 specularReflection= _Color.rgb * pow(max(0.0,rate*rate),_Shininess);

            //float lightdir = saturate(dot(worldNormal,worldLightDir));
            //前文计算好的漫反射光
            float3 diffuseReflection=  _Color.rgb* max(0.0, dot(worldNormal, worldLightDir));

            //环境光直接获取
            float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT * _Color;
            //return float4(ambientLighting + diffuseReflection+ specularReflection, 1.0);
            return float4(specularReflection * 3 , 1);
            */
        }

        fixed4 frag(v2f i) : SV_Target {  
            return getcolor(i)  ;    
        }      
    ENDCG  

    SubShader {
         

        Pass{    
            Tags { "RenderType"="Opaque"  "LightMode" = "ForwardBase"}
            LOD 200  
            CGPROGRAM  
 

            #pragma vertex vert      
            #pragma fragment frag  
            ENDCG   
        }  
         
        Pass{    
            Tags { "RenderType"="Opaque"  "LightMode" = "ForwardAdd"}
            Blend One One
 
            ZWrite Off
            LOD 200  
            CGPROGRAM  

            #define POINT
        
            #include "Autolight.cginc" 
            #include "UnityPBSLighting.cginc"
            #pragma vertex vert      
            #pragma fragment frag2
            fixed4 frag2(v2f i) : SV_Target {  
                float4 color = getcolor(i) ;

                UNITY_LIGHT_ATTENUATION(attenuation, 0, i.posWorld);

                return color * (attenuation);
            }   

            ENDCG   
        }   

    }
    FallBack "Diffuse"
}
