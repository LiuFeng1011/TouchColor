using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBaseObj : MSBaseObject {

    public enum GameObjState{
        white,
        black,
        red
    }

    GameObjState mystate = GameObjState.white;

    protected Material m;

    float colorAction = 0f,colorMaxAction = 0.5f;
    Color targetColor = Color.white,lastColor = Color.white;

    bool isdie = false;

    public override void ObjUpdate(){
        if (colorAction <= 0) return;

        colorAction -= Time.deltaTime;
        if (colorAction < 0) colorAction = 0;
        m.color = Color.Lerp(targetColor,lastColor, colorAction / colorMaxAction);
    }

    public virtual void SetDie(){
        isdie = true;
    }

    public virtual bool IsDie()
    {
        return isdie;
    }

    public virtual void Die(){
        Destroy(gameObject);
    }

    public GameObjState GetMyState(){
        return mystate;
    }

    public void SetState(GameObjState state){
        mystate = state;
        if(m != null){
            lastColor = targetColor;
            if(state == GameObjState.black) targetColor = new Color(0,0,0);
            else targetColor = new Color(1, 1, 1);
            colorAction = colorMaxAction;
            Debug.Log(targetColor);
        }
    }
}
