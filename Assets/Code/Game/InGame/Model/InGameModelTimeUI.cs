using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameModelTimeUI : MonoBehaviour {

    UILabel time;
    UISprite progress;

    int lastVal = 0,maxVal;
	// Use this for initialization
	void Start () {
	}

    public void Init(int maxval){
        this.maxVal = maxval;
        lastVal = maxval;

        time = transform.Find("obj").Find("Time").GetComponent<UILabel>();
        progress = transform.Find("obj").Find("progress").GetComponent<UISprite>();
    }
	
    public void SetVal(float val){
        if (val < 0) val = 0;
        if(lastVal == (int)val){
            return;
        }
        lastVal = (int)val;

        string timestring = string.Format("{0:D2}:{1:D2}", lastVal / 60, lastVal % 60);

        time.text = timestring;

        float scale = Mathf.Min((float)lastVal / (float)maxVal,1);

        progress.transform.localScale = new Vector3(scale, 1, 1);
    }
}
