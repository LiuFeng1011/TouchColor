using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapObjectConf
{
    public int objid;    /*  道具id    */
    public string name; /*  道具名 */
    public string prefabName;   /*  预制体 */
    public int type;/*   "种类
    1角色
    2敌人
    3子弹
    4装饰" 
    */
    public float speed; /*  移动速度    */
    public int life;    /* 生命 */
    public int dieeffect;    /*  死亡特效    */
}


public class ConfMapObjectManager{
    public List<MapObjectConf> datas {get;private set;}
    public Dictionary<int, MapObjectConf> dic = new Dictionary<int, MapObjectConf>();

    //key : obj type 
    //val : obj
    public Dictionary<int, List<MapObjectConf>> dicByType = new Dictionary<int, List<MapObjectConf>>();

	public void Load(){

		if(datas != null) datas.Clear();

        datas = ConfigManager.Load<MapObjectConf>();
        dic.Clear();

        for (int i = 0; i < datas.Count; i++)
        {
            MapObjectConf obj = datas[i];
            dic.Add(obj.objid, obj);
            if(!dicByType.ContainsKey(obj.type)){
                List<MapObjectConf> typelist = new List<MapObjectConf>();
                typelist.Add(obj);
                dicByType.Add(obj.type,typelist);
            }else{
                dicByType[obj.type].Add(obj);
            }
        }
	}

    public MapObjectConf GetRandomObjByType(int type){
        List<MapObjectConf> list ;
        if(!dicByType.TryGetValue(type,out list)){
            return null;
        } 
        return list[(int)UnityEngine.Random.Range(0, list.Count)];
    }

}
