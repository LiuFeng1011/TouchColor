using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboFlyObj : MonoBehaviour {

    Vector3[] positions;
    float time = 0f, maxTime = 2.0f;

    public static ComboFlyObj Create(Vector3[] ps){
        if(ps.Length < 4){
            return null;
        }
        GameObject go = Resources.Load("Prefabs/MapObj/ComboFlyObj") as GameObject;
        GameObject tempObj = (GameObject)Instantiate(go);
        tempObj.transform.position = ps[0];
        ComboFlyObj cf = tempObj.transform.GetComponent<ComboFlyObj>();
        cf.Init(ps);


        return cf;
    }

    void Init(Vector3[] ps){
        this.positions = ps;
        (new EventCreateEffect(60010015, gameObject, transform.position, 1.0f)).Send();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        float scale = time / maxTime;

        if(scale >= 1){
            Destroy(gameObject);
            return;
        }

        Vector3 nowPos = new Vector3();
        nowPos.x = GetBezierat(positions[0].x,positions[1].x, positions[2].x, positions[3].x,scale);
        nowPos.y = GetBezierat(positions[0].y, positions[1].y, positions[2].y, positions[3].y, scale);
        nowPos.z = 0;

        transform.position = nowPos;

	}

    public static float GetBezierat(float a, float b, float c, float d, float t)  
    {  
        return (Mathf.Pow(1 - t, 3) * a +  
                3 * t * (Mathf.Pow(1 - t, 2)) * b +  
                3 * Mathf.Pow(t, 2) * (1 - t) * c +  
                Mathf.Pow(t, 3) * d);  
    }  
}
