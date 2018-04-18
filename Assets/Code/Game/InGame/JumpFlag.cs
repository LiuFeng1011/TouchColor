using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlag : MonoBehaviour {
    float time, speed = 5, hight = 0.3f,basey;
	// Use this for initialization
	void Start () {
        basey = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * speed;

        transform.localPosition = new Vector3(transform.localPosition.x,
                                              Mathf.Abs(Mathf.Sin(time)) * hight + basey,
                                              transform.localPosition.z);
	}
}
