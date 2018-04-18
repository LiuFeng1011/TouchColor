using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadScoresLabel : MonoBehaviour {

    public float labelActionTime = 0f, labelActionMaxTime = 0.5f, toTargetTime = 0f, toTargetDeltaTime = 0.2f;
    public float actionScale = 0.5f;
    UILabel scoreslabel;
    Vector3 baseLabelScale;

    int nowScores, targetScores;

    public void Init(int initscores){
        nowScores = targetScores = initscores;
    }
	// Use this for initialization
	void Start () {
        scoreslabel = gameObject.GetComponent<UILabel>();
        baseLabelScale = scoreslabel.transform.localScale;
        scoreslabel.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        if (labelActionTime > 0) LabelAction();

        if(nowScores != targetScores) ToTarget();
	}

    void LabelAction()
    {
        labelActionTime = Mathf.Max(labelActionTime - Time.deltaTime, 0);
        float rate = 1 - labelActionTime / labelActionMaxTime;
        float scale = Mathf.Sin((3.14f) * rate) * actionScale;
        scoreslabel.transform.localScale = baseLabelScale + new Vector3(scale, scale, scale);
    }

    void ToTarget(){
        toTargetTime += Time.deltaTime;

        if (toTargetTime < toTargetDeltaTime) return;

        toTargetTime = 0f;
        nowScores += (int)Mathf.Ceil((float)(targetScores - nowScores) * 0.3f);
        nowScores = Mathf.Min(nowScores, targetScores);

        labelActionTime = labelActionMaxTime;

        scoreslabel.text = ""+nowScores;
    }


    public void SetScores(int val)
    {
        targetScores = val;
        toTargetTime = 1.0f;
    }

}
