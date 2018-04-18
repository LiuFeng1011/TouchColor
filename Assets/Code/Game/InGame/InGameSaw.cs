using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSaw : InGameBaseObj {

    float time = 0f;

    public override void ObjUpdate()
    {
        time += Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, time * 180);

        //碰撞检测
        if (Vector3.Distance(transform.position, InGameManager.GetInstance().role.transform.position) <
           (transform.localScale.x + InGameManager.GetInstance().role.transform.localScale.x) * 0.35)
        {
            InGameManager.GetInstance().role.Die();
        }
    }



}
