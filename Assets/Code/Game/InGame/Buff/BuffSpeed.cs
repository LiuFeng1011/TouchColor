using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : BaseBuff {
    //60010011
    GameObject effectobj;
    public override void Init(BuffType type,float time, float val)
    {
        base.Init(type,time,val);

        effectobj = new GameObject("role speed effect");
        effectobj.transform.parent = InGameManager.GetInstance().role.transform;

        effectobj.transform.localPosition = Vector3.zero;

        (new EventCreateEffect(60010011, effectobj, effectobj.transform.position, 1.0f)).Send();
    }


    public override float GetProperty(BuffProperty property)
    {
        if(property == BuffProperty.speed){
            return GameConst.JUMP_FORCE * val;
        }
        return 0;
    }

    public override void Update()
    {
        base.Update();

    }

    public override void Destory()
    {
        base.Destory();
        MonoBehaviour.Destroy(effectobj);
    }
}
