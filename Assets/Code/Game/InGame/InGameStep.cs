using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStep : InGameBaseObj {

	public override void Init()
    {
        base.Init();

        m = transform.Find("icon").GetComponent<Renderer>().material;  

        SetState(Random.Range(0f, 1f) < 0.5f ? GameObjState.black : GameObjState.white);
    }
	
	// Update is called once per frame
	public override void ObjUpdate()
    {
        base.ObjUpdate();
        transform.position -= new Vector3(0, Time.deltaTime*3, 0);
    }

    public override void Die()
    {

        base.Die();
    }

}
