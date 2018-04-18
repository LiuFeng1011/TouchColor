using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBgColor : BaseGameObject {

    public void Init(){
        float rand = Random.Range(0f, 1f);
        float h, s, v;
        h = rand;
        s = 0.8f;
        v = 0.8f;
        Camera.main.backgroundColor = Color.HSVToRGB(h - (int)h, s, v);
    }

    public void Update(){
        
    }

    public void Destroy(){
        
    }
}
