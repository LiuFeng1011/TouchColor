Shader "Custom/CameraFog" {

    Properties {    
        _MainTex ("Base (RGB)", 2D) = "white" {}    
        _Camber ("Camber", float) = 0.5
        _Radius ("Radius", float) = 5
    }  
    SubShader {    
        Tags { "RenderType"="Opaque" }    
        LOD 200    
    
        Pass{    
            CGPROGRAM    
   
            #include "UnityCG.cginc"    
            #pragma vertex vert_img    
            #pragma fragment frag    
  
            uniform sampler2D _MainTex;    
            uniform float _Camber;
            uniform float _Radius;

            float4 frag( v2f_img o ) : COLOR    
            {    
                //获取世界坐标  
                fixed4 renderTex = tex2D(_MainTex, o.uv);    

                fixed rate = clamp  
                        (  
                            pow(abs(o.uv.x-0.5) / _Camber, _Radius) +   
                            pow(abs(o.uv.y-0.5) / _Camber, _Radius),  
                            0,   
                            0.5  
                        );  

                return renderTex * (1-rate) ;  
            }    
            ENDCG    
        }    
    }     
	FallBack "Diffuse"
}
