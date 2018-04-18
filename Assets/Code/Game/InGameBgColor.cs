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
        Color c = Color.HSVToRGB(h - (int)h, s, v);
        Camera.main.backgroundColor = c;

        GameObject bg = GameObject.Find("bg");
        if (bg == null) return;
        ParticleSystem particle = bg.GetComponent<ParticleSystem>();

        var shape = particle.shape;
        shape.box = new Vector3(
            InGameManager.GetInstance().GetGameRect().width,
            0,InGameManager.GetInstance().GetGameRect().height);

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(c, 0.0f) , new GradientColorKey(c, 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f) , new GradientAlphaKey(0.1f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) });
        var a = particle.colorOverLifetime;
        a.color = grad;
    }

    public void Update(){
        
    }

    public void Destroy(){
        
    }
}
