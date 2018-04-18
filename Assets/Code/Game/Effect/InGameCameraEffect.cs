using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] 
public class InGameCameraEffect : MonoBehaviour {

    public Material m;

    public float _Camber = 0.75f, _Radius = 3.15f;
  
    Camera mainCamera;  
  
	// Use this for initialization
	void Start () {
        m = new Material(Shader.Find("Custom/CameraFog"));  

        m.SetFloat("_Camber", _Camber);  
        m.SetFloat("_Radius", _Radius);  
	}
	
	// Update is called once per frame
	void Update () {


	}

    void OnRenderImage(RenderTexture src, RenderTexture dest)  
    {  
        Graphics.Blit(src, dest, m);  
    }  
}
