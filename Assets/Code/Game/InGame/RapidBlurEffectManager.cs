using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidBlurEffectManager : BaseUnityObject {

    int blurVal = 0,maxBlurVal = 8,dir = 1;
    float updateInterval = 0.1f, updateTime = 0f;

    RapidBlurEffect effect;
	
    public void StartBlur(){
        if(effect == null){
            effect = InGameManager.GetInstance().gamecamera.gameObject.AddComponent<RapidBlurEffect>();
        }
        blurVal = 0;
        effect.BlurSpreadSize = 0;
        effect.DownSampleNum = 0;
        effect.BlurIterations = blurVal;

        dir = 1;
    }

    public void OverBlur(){
        if (effect == null)
        {
            return;
        }
        dir = -1;
    }

    private void Update()
    {
        updateTime += Time.deltaTime;
        if(updateTime < updateInterval){
            return;
        }
        updateTime = 0f;

        blurVal += dir;
        if(dir > 0 ){
            if(blurVal >= maxBlurVal){
                blurVal = maxBlurVal;
            }
        }else{
            if(blurVal <= 0){
                Destroy(effect);
                effect = null;
            }
        }

        if(effect != null){
            effect.BlurIterations = blurVal;
        }
    }
}
