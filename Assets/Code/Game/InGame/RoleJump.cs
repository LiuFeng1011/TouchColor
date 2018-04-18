using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleJump : MonoBehaviour {
    Vector3 targetPos;
    Vector3 startPos;
    float distance;
    float speed;
    float moveTime = 0f;
    float maxTime;

    public bool isStart = false, isfull = false,isfinished = false;

    public void JumpStart(Vector3 startPos, Vector3 targetPos, float speed){
        this.targetPos = targetPos;
        this.startPos = startPos;
        this.speed = speed;
        distance = Vector3.Distance(startPos, targetPos);

        maxTime = Vector3.Distance(targetPos, startPos) / speed;

        transform.position = startPos;
        //transform.forward = targetPos - startPos;

        moveTime = 0f;
        isStart = true;
        isfull = false;
        isfinished = false;
    }

    public void Stop(){
        isStart = false;
    }
	
	// Update is called once per frame
    public void JumpUpdate () {
        if (!isStart) return;

        moveTime += Time.deltaTime;

        if(!isfull){
            if (moveTime > maxTime){
                moveTime = maxTime;
                isfull = true;

            }
        }else if(!isfinished ){
            isfinished = true;
            return;
        }

        float rate = moveTime / maxTime;
        Vector3 pos = startPos + (targetPos - startPos) * (rate);
        pos.y = JumpFormula(distance * rate, distance, distance * 0.3f,0);
        this.transform.position = pos;

	}

    //抛物线
    static float GetJumpFormulaW(float baseh, float h, float x){
        float m = (2 * h * x) / baseh;
        return Mathf.Sqrt(m * (2 * x + m)) - m;
    }
    public static float JumpFormula(float x, float w, float h, float baseh){
        if(baseh != 0){
            w = GetJumpFormulaW(baseh,h,w);
        }

        return -(x-w) * x * (h/Mathf.Pow((w/2),2)) + baseh;  
    }  
}
