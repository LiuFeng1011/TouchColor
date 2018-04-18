using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLayerManager : InGameUIBaseLayer {


    public override void Init()
    {
        base.Init();
        GameObject homeBtn = transform.Find("homeBtn").gameObject;
        GameUIEventListener.Get(homeBtn).onClick = HomeBtnCB;

        GameObject nextBtn = transform.Find("nextBtn").gameObject;
        GameUIEventListener.Get(nextBtn).onClick = NextBtnCB;

    }


    void HomeBtnCB(GameObject obj)
    {
        (new EventChangeScene(GameSceneManager.SceneTag.Menu)).Send();
        gameObject.SetActive(false);
    }

    void NextBtnCB(GameObject obj)
    {
        (new EventChangeScene(GameSceneManager.SceneTag.Game)).Send();
        gameObject.SetActive(false);
    }
}
