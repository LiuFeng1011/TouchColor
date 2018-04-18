using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairEffect : MonoBehaviour {


    public static StairEffect Create(Vector3 pos, float dis){

        GameObject column = (GameObject)Resources.Load("Prefabs/StairEffect");

        column = MonoBehaviour.Instantiate(column);
        StairEffect effect = column.GetComponent<StairEffect>();

        return effect;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
