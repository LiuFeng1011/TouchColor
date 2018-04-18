using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : BaseGameObject
{
    bool isdie = false;
    public enum BuffType
    {
        speed,
        invincible,
        magent,
        scale
    }

    public enum BuffProperty{
        speed,
        hurt,
    }

    BuffType type;
    protected float time, val,maxtime;

    public virtual void Init(BuffType type,float time,float val)
    {
        this.type = type;
        this.time = this.maxtime = time;
        this.val = val;
    }

    public virtual void Reset(float time, float val){
        this.time = this.maxtime = time;
        this.val = val;
    }

    public virtual void Update(){
        time -= Time.deltaTime;

        if(time < 0){
            isdie = true;
        }
    }

    public virtual void Destory(){
        
    }

    public virtual float GetProperty(BuffProperty property){
        return 0f;
    }

    public bool IsDie(){
        return isdie;
    }

    public BuffType GetBuffType(){
        return type;
    }

}
