using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStep : InGameBaseObj {

	public override void Init()
    {
        base.Init();

        m = transform.Find("icon").GetComponent<Renderer>().material;

        SetState(Random.Range(0f,1f) < 0.5f ? GameObjState.white : GameObjState.black);

    }
	
	// Update is called once per frame
	public override void ObjUpdate()
    {
        base.ObjUpdate();
        transform.position -= new Vector3(0, Time.deltaTime*InGameManager.GetInstance().gameSpeed, 0);
    }

    public override void Die()
    {
        //60010012
        if (GetMyState() == GameObjState.black)
        {
            (new EventCreateEffect(60010012, null, transform.position, 1.0f)).Send();
        }
        else if (GetMyState() == GameObjState.white)
        {
            (new EventCreateEffect(60010013, null, transform.position, 1.0f)).Send();
        }
        base.Die();
    }

}
