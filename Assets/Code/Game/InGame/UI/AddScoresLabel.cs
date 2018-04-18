using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoresLabel : MonoBehaviour {

    const float maxTime = 1.0f, maxHight = 0.1f;

    Vector3 startPos;
    UILabel label;

    public AnimationCurve ac;

    float time;

    public void Init(Vector3 startPos,int scores){
        this.startPos = startPos;
        label = transform.GetComponent<UILabel>();
        label.text = "+" + scores;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if(time > maxTime){
            Destroy(gameObject);
        }

        float rate = time / maxTime;
        float val = ac.Evaluate(rate);

        transform.position = startPos + new Vector3(0,maxHight * val, 0);
        label.color = new Color(label.color.r, label.color.g, label.color.b, 1 - val);
	}


}
