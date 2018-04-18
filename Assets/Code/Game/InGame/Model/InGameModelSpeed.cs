using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameModelSpeed : InGameBaseModel {
    float speed = 0.5f, addSpeedTime = 5f, addSpeedStep = 0.1f;

    GameObject plane; 
    public override void Init()
    {
        plane = Resources.Load("Prefabs/MapObj/SpeeModelPlane") as GameObject;
        plane = MonoBehaviour.Instantiate(plane);

        plane.transform.position = new Vector3(-5, 0.2f,0);

    }

    public override void Update()
    {
        base.Update();
        addSpeedTime -= Time.deltaTime;
        if(addSpeedTime < 0  && speed < 2){
            addSpeedTime = 1f;
            speed += addSpeedStep;
        }

        plane.transform.position = new Vector3(plane.transform.position.x + Time.deltaTime * speed,
                                               plane.transform.position.y, plane.transform.position.z);

        if (plane.transform.position.x > InGameManager.GetInstance().role.transform.position.x){
            InGameManager.GetInstance().role.Die();
        }

    }

    public override void Revive()
    {
        plane.transform.position = new Vector3(plane.transform.position.x - 5,
                                               plane.transform.position.y, plane.transform.position.z);
    }
}
