using UnityEngine;
using System.Collections;

public class BaseUnityObject : MonoBehaviour,EventObserver {

	// Use this for initialization
    public virtual void Init () {
	
	}
	
	// Update is called once per frame
    public virtual void  ObjUpdate () {
	    
	}

	public virtual void HandleEvent(EventData resp){

	}
}
