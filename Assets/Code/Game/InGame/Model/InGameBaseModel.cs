using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBaseModel : BaseGameObject {

    public static InGameBaseModel Create(int model){

        //switch(model){
        //    case 0:
        //        return new InGameBaseModel();
        //    case 1: 
        //        return new InGameModelSpeed();
        //    case 2:
        //        return new InGameModelTime();
                
        //}

        return new InGameBaseModel();
    } 


    public virtual void Init()
    {

    }

    public virtual void Update(){
        
    }

    public virtual void Revive(){
        
    }

    public void Destroy()
    {

    }
}
