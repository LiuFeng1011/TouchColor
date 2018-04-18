using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Map data.
/// 关卡存档数据
/// 存储和载入关卡都依靠此类进行
/// </summary>
public class MapData {
    public MSLevelOptionDataModel od = new MSLevelOptionDataModel();
    public Dictionary<int, MSBaseObject> dic = new Dictionary<int, MSBaseObject>();

    public virtual void Serialize(DataStream datastream)
    {
        
	}

    public virtual void Deserialize(DataStream datastream)
	{
        od.deserialize(datastream);

        GameObject tGO = new GameObject("LevelOption");
        LevelOption me = tGO.AddComponent<LevelOption>();

        od.SetLevelOption(me);

        GameCommon.LOAD_DATA_VERSION = od.version;

        int objcount = datastream.ReadSInt32();

        for (int i = 0; i < objcount; i++)
        {
            int type = datastream.ReadSInt32();
            string id = datastream.ReadString16();

            GameObject go;

            if (type == -1)
            {
                //组物体
                go = new GameObject("LevelOption");
            }
            else
            {

                Object tempObj = Resources.Load("Prefabs/MapObj/" + id) as GameObject;
                if (tempObj == null)
                {
                    Debug.Log(type + "/" + id + " is null!");
                    return;
                }
                go = (GameObject)MonoBehaviour.Instantiate(tempObj);
            }

            MSBaseObject baseobj = go.GetComponent<MSBaseObject>();

            if (baseobj == null)
            {
                baseobj = go.AddComponent<MSBaseObject>();
            }
            //baseobj.itemtype = type;
            baseobj.itemid = id;

            baseobj.Deserialize(datastream);
            baseobj.init();
            baseobj.Init();


            if (dic.ContainsKey(baseobj.parent))
            {
                go.transform.parent = dic[baseobj.parent].transform;
            }
            else
            {
                go.transform.parent = me.transform;
            }

            dic.Add(baseobj.myData.instanceID, baseobj);
        }
	}

}
