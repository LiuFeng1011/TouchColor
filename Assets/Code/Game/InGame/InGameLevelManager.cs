using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLevelManager : BaseGameObject {

    public List<InGameBaseObj> objList = new List<InGameBaseObj>();
    public List<InGameBaseObj> addList = new List<InGameBaseObj>();
    public List<InGameBaseObj> delList = new List<InGameBaseObj>();

    InGameCreateObjManager inGameCreateObjManager;
    InGameStoryMapManager inGameStoryMapManager;

    float addStepDis = 0;

    public void Init(){
        if(UserDataManager.selLevel == null){
            inGameCreateObjManager = new InGameCreateObjManager();
            inGameCreateObjManager.Init();
        }else {

            inGameStoryMapManager = new InGameStoryMapManager();
            inGameStoryMapManager.Start(InGameManager.GetInstance().md);
        }

    }

    public void Update(){

        if(inGameCreateObjManager != null)inGameCreateObjManager.Update();
        if (inGameStoryMapManager != null) inGameStoryMapManager.Update();

        for (int i = 0; i < objList.Count; i++)
        {
            InGameBaseObj obj = objList[i];
            obj.ObjUpdate();

            if (obj.IsDie())
            {
                delList.Add(obj);
            }
        }

        for (int i = 0; i < delList.Count; i++)
        {
            InGameBaseObj obj = delList[i];
            objList.Remove(obj);
            obj.Die();
        }
        delList.Clear();

        if (addList.Count > 0)
        {
            objList.AddRange(addList);
            addList.Clear();
        }

    }

    public InGameBaseObj AddObj(string id){

        InGameBaseObj objscript = MSBaseObject.CreateById(id) as InGameBaseObj;
        addList.Add(objscript);
        return objscript;
    }
    public void AddObj(InGameBaseObj obj)
    {
        addList.Add(obj);
    }

    public void ClearObj(){
        for (int i = 0; i < objList.Count; i++)
        {
            objList[i].SetDie();
        }
    }

    public void Destroy(){
        if (inGameCreateObjManager != null)inGameCreateObjManager.Destroy();
        if (inGameStoryMapManager != null) inGameStoryMapManager.OnDestroy();
    }
}
