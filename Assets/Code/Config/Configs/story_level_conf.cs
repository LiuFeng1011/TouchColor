using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class story_level_conf{
    public int id;  /*  id(int) */
    public int group;   /*  章节(int) */
    public int index;   /*  序号(int) */
    public string name; /*  名字(string)  */
    public string description; /*  描述(string)  */
    public string file_path;    /*  文件名(string) */
    public string icon; /*  图标(string)  */
}
public class StoryLevelManager
{
    List<story_level_conf> datas = new List<story_level_conf>();
    Dictionary<int, story_level_conf> dic = new Dictionary<int, story_level_conf>();
    public void Load()
    {
        if (datas != null) datas.Clear();
        dic.Clear();

        List<story_level_conf> _datas = ConfigManager.Load<story_level_conf>();

        for (int i = 0; i < _datas.Count; i++)
        {
            dic.Add(_datas[i].id, _datas[i]);
            bool isfind = false;
            for (int j = 0; j < datas.Count; j++)
            {
                if (_datas[i].index < datas[j].index)
                {
                    datas.Insert(j, _datas[i]);
                    isfind = true;
                    break;
                }
            }
            if (!isfind) datas.Add(_datas[i]);
        }
    }

    public List<story_level_conf> GetDataList()
    {
        return datas;
    }

    public story_level_conf GetData(int id){
        return dic[id];
    }
}