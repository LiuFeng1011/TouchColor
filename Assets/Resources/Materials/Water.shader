Shader "Custom/Water" {  
    Properties {  
        _Color ("Color", Color) = (1,1,1,1)  
        _BackColor ("BackColor", Color) = (1,1,1,1)  
        _MainTex ("Main Tex", 2D) = "white" {}  
        _Magnitude ("Distortion Magnitude" , Float) = 1  
        _Frequency ("Distortion Frequency" , Float) = 1  
        _InvWaveLength("Distortion Inverse Wave Length",Float) = 10  
        _Speed("Speed",Float) = 0.5  
    }  
    SubShader {  
        Tags{"IgnoreProjector"="True"  "Queue"="Transparent" "RenderType"="Transparent"}  
          
        Pass{  
            CGPROGRAM  
 
            #pragma vertex vert  
            #pragma geometry geom  
            #pragma fragment frag  
 
            #include "Lighting.cginc"  
  
            sampler2D _MainTex;  
            float4 _MainTex_ST;  
            fixed4 _Color;  
            fixed4 _BackColor;
            float _Magnitude;  
            float _Frequency;  
            float _InvWaveLength;  
            float _Speed;  
  
            struct a2v{  
                float4 vertex : POSITION;  
                float3 normal : NORMAL;  
                float4 texcoord : TEXCOORD0;  
            };  
  
            struct v2f {  
                float4 pos : SV_POSITION;  
                float3 worldNormal : TEXCOORD0;  
                float3 worldPos : TEXCOORD1;  
                float2 uv : TEXCOORD2;  
                float4 color : TEXCOORD3; 
            };  
  
            v2f vert(a2v v) {  
                v2f o;  
                float4 offset;  
                offset.xyzw = float4(0,0,0,0);  
                
                o.worldPos =  mul(unity_ObjectToWorld,v.vertex).xyz;  
  
                float d2 = tex2Dlod(_MainTex,float4(v.texcoord.xy,0,0)).r * 3;

                //float sinx = sin(_Frequency * _Time.y + v.vertex.x * _InvWaveLength + v.vertex.y * _InvWaveLength + v.vertex.z * _InvWaveLength);  
                offset.y = sin((d2) * (_Time.y + 100) * 1.5) * 0.5  + sin((_Time.y +(v.vertex.x + v.vertex.y) * 0.2) * 2)*0.5;  
                //offset.x =  v.vertex.x + sin(_Time.y );  
                //offset.z =  v.vertex.z + sin(_Time.y );  
                if(v.texcoord.x == 0){
                    offset.y = -0.5;
                }
  
                o.pos = UnityObjectToClipPos(v.vertex+offset); 
                o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);  、=
                o.color = float4(v.texcoord.x,0,0,1);
                return o;  
            }  
  
            [maxvertexcount(3)]  
            void geom(triangle v2f IN[3], inout TriangleStream<v2f> triStream)  
            {  
                float3 v0 = IN[0].pos.xyz;  
                float3 v1 = IN[1].pos.xyz;  
                float3 v2 = IN[2].pos.xyz;  
  
                float3 vn = normalize(cross(v1 - v0, v2 - v0));  
  
                vn = UnityObjectToWorldNormal(vn);  
  
                v2f OUT;  
                OUT.pos = IN[0].pos;  
                OUT.worldNormal = vn;  
                OUT.worldPos = IN[0].worldPos;  
                OUT.uv = IN[0].uv;  
                OUT.color = IN[0].color; 
                triStream.Append(OUT);  
  
                OUT.pos = IN[1].pos;  
                OUT.worldNormal = vn;  
                OUT.worldPos = IN[1].worldPos;  
                OUT.uv = IN[1].uv;  
                OUT.color = IN[1].color; 
                triStream.Append(OUT);  
  
                OUT.pos = IN[2].pos;  
                OUT.worldNormal = vn;  
                OUT.worldPos = IN[2].worldPos;  
                OUT.uv = IN[2].uv;  
                OUT.color = IN[2].color; 
                triStream.Append(OUT);  
  
            }  
            fixed4 frag(v2f i) : SV_Target {  

                float3 viewDirection = normalize(_WorldSpaceCameraPos - i.worldPos);

                //获取法线方向  
                fixed3 worldNormal = normalize(i.worldNormal);  
                //灯光方向  
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos)); 
                 
                float r = dot(viewDirection,worldNormal) - dot(worldLightDir,worldNormal);

                float d = dot(worldLightDir,worldNormal);
                return  lerp(_Color,_BackColor,d *d) ;
            }  
            ENDCG  
        }  
    }  
    FallBack "Transparent/VertexLit"  
}  