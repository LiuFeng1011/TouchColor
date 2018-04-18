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

    bool isdie = false;

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
            if(state == GameObjState.black) m.color = new Color(0, 0, 0);
            else m.color = new Color(1, 1, 1);
        }
    }
}
