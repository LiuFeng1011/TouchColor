using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameModelTime : InGameBaseModel {

    InGameModelTimeUI timeUI;
    float time;
    public override void Init()
    {
        time = GameConst.timeModelTime;
            
        GameObject gamepad = GameObject.Find("UI Root").transform.Find("GamePad").gameObject;

        GameObject timeobj = NGUITools.AddChild(gamepad, Resources.Load("Prefabs/UI/Time") as GameObject);
        timeUI = timeobj.GetComponent<InGameModelTimeUI>();

        timeUI.Init((int)time);
    }

    public override void Update()
    {
        if (time <= 0) return;
        time -= Time.deltaTime;
        timeUI.SetVal(time);
        if(time <= 0){
            InGameManager.GetInstance().role.Die();
        }
    }


    public override void Revive()
    {
        if(time <= 0){
            time += 15f;
        }
    }

}
