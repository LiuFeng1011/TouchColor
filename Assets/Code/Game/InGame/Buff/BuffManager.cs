using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : BaseGameObject {

    List<BaseBuff> buffList = new List<BaseBuff>();
    List<BaseBuff> delList = new List<BaseBuff>();
    List<BaseBuff> addList = new List<BaseBuff>();

    public void Init(){
        
    }

    public void Update(){
        for (int i = 0; i < buffList.Count;i  ++ ){
            buffList[i].Update();

            if(buffList[i].IsDie()){
                delList.Add(buffList[i]);
            }
        }

        for (int i = 0; i < delList.Count; i++)
        { 
            buffList.Remove(delList[i]);
            delList[i].Destory();
        }
        delList.Clear();

        if(addList.Count > 0){
            buffList.AddRange(addList);
            addList.Clear();
        }

    }
    
    public void Destroy()
    {

    }

    public void AddBuff(BaseBuff.BuffType type, float time, float val){

        for (int i = 0; i < buffList.Count;i ++){
            if(buffList[i].GetBuffType() == type){
                buffList[i].Reset(time, val);
                return;
            }
        }

        BaseBuff buff = null;
        switch(type){
            case BaseBuff.BuffType.speed:
                buff = new BuffSpeed();
                break;
            default:
                Debug.LogError("no buff :"+ type);
                break;
        }
        buff.Init(type,time,val);
        addList.Add(buff); 
    }

    public float GetProperty(BaseBuff.BuffProperty property){
        float ret = 0f;
        for (int i = 0; i < buffList.Count; i++)
        {
            ret += buffList[i].GetProperty(property);
        }

        return ret;
    }
}
