using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadManager : InGameUIBaseLayer {

    GamePadScoresLabel scoreslabel;
    public override void Init()
    {
        base.Init();
        scoreslabel = transform.Find("scores").Find("Label").GetComponent<GamePadScoresLabel>();

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
       
	}

    public void SetScores(int val)
    {
        scoreslabel.SetScores(val);
    }

}
