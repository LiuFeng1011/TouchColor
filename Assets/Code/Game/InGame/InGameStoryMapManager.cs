using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStoryMapManager : BaseGameObject{
    MapData md;

    List<MSBaseObject> objList = new List<MSBaseObject>();

    int addindex = 0;
    int delindex = 0;
    public void Start(MapData md)
    {
        this.md = md;

        foreach (KeyValuePair<int, MSBaseObject> pair in md.dic)  
        {
            MSBaseObject obj = pair.Value;
            bool isadd = false;

            if(obj.itemid == "group"){
                continue;
            }
            //obj.gameObject.SetActive(false);
            for (int i = 0; i < objList.Count; i ++){
                MSBaseObject _obj = objList[i];
                if(obj.transform.position.y < _obj.transform.position.y){
                    objList.Insert(i,obj);
                    isadd = true;
                    break;
                }
            }

            if(!isadd){
                objList.Add(obj);
            }
        }
    }
	// Update is called once per frame
    public void Update () {

        Rect gamerect = InGameManager.GetInstance().GetGameRect();
        while(addindex < objList.Count && objList[addindex].transform.position.y - 1< gamerect.y + gamerect.height){
            InGameManager.GetInstance().inGameLevelManager.AddObj((InGameBaseObj)objList[addindex]);
            addindex++;
        }

        //for (int i = 0; i < objList.Count; i++)
        //{
        //    InGameBaseObj obj = (InGameBaseObj)objList[i];
        //    obj.ObjUpdate();

        //    if (obj.IsDie())
        //    {
        //        delList.Add(obj);
        //    }
        //}

        //for (int i = 0; i < delList.Count; i++)
        //{
        //    InGameBaseObj obj = delList[i];
        //    objList.Remove(obj);
        //    obj.Die();
        //}
        //delList.Clear();

        //InGameManager.GetInstance().gamecamera.transform.position += new Vector3(0, Time.deltaTime, 0);
	}

    public void OnDestroy()
    {
        EventManager.Remove(this);
    }

}
