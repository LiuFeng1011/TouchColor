using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// author:liufeng
/// date:170812
/// 游戏内特效管理器
/// </summary>
public class GameEffectManager : BaseGameObject{
	//特效池
	Dictionary<int ,List<GameEffect>> effectPool = new Dictionary<int ,List<GameEffect>>();

	public GameEffectManager(){

		//创建特效,加入到池里
        List<GameEffectData> _conf = ConfigManager.confEffectManager.datas;

		for(int i = 0 ; i < _conf.Count ; i ++){
            GameEffectData conf = _conf[i];
			for(int j = 0 ; j < conf.repeat_count ; j++){
				GameEffect eff = CreateEffect(conf.id);
				if(eff != null){
					if(!effectPool.ContainsKey(conf.id)){
						effectPool.Add(conf.id,new List<GameEffect>());
					}
					effectPool[conf.id].Add(eff);

				}
			}
		}

        EventManager.Register(this, EventID.EVENT_CREATE_EFFECT);
	}


	/// <summary>
	/// 添加特效到世界
	/// </summary>
	/// <returns>The world effect.</returns>
	/// <param name="effectid">Effectid.</param>
	/// <param name="pos">Position.</param>
    GameEffect AddWorldEffect(int effectid,Vector3 pos, float scale){
		GameEffect ge = GetEffect(effectid,pos);
		if(ge == null) return null;
		ge.transform.position = pos;
        ge.Play(scale);
		return ge;
	}

	/// <summary>
	/// 添加特效到服务器
	/// </summary>
	/// <returns>The world effect.</returns>
	/// <param name="effectid">Effectid.</param>
	/// <param name="pos">Position.</param>
	GameEffect AddEffect(int effectid, GameObject obj, Vector3 pos, float scale){
        if(obj == null){
            return AddWorldEffect(effectid,pos,scale);
        }
		GameEffect ge = GetEffect(effectid,obj.transform.position + pos);
		if(ge == null) return null;
        ge.SetParent(obj);
        ge.Play(scale);
		return ge;
	}

	GameEffect GetEffect(int effectid ,Vector3 worldPos){
		//对应池是否存在
		if(!effectPool.ContainsKey(effectid)){
			return null;
		}


		//寻找空闲特效
		List<GameEffect> pool = effectPool[effectid];
		GameEffect ret = null;
		for(int i = 0 ; i < pool.Count ; i ++){
			GameEffect eff = pool[i];

			if(eff.gameObject.activeSelf){
				continue;
			}

			ret = eff;
			break;
		}

		return ret;
	}

	GameEffect CreateEffect(int effectid){
        GameEffectData _conf = ConfigManager.confEffectManager.GetData(effectid);
        Object obj = Resources.Load(_conf.file_path);
		if(obj == null){
			Debug.LogError("cant find file ! : " + _conf.file_path);
		}
		if(_conf.res_type == 1){
			GameObject go = (GameObject)MonoBehaviour.Instantiate(obj);
            go.transform.position = new Vector3(-999999, 0, 0);
			GameEffect ge = go.AddComponent<GameEffect>();
			ge.Init(_conf);

			return ge;
		}
		return null;
	}

    public override void HandleEvent(EventData resp)
    {
        switch(resp.eid){
            case EventID.EVENT_CREATE_EFFECT:
                EventCreateEffect ceeve = (EventCreateEffect)resp;
                AddEffect(ceeve.effectid, ceeve.obj, ceeve.pos, ceeve.scale);
                break;
        }
    }

    public void Destroy()
    {
        EventManager.Remove(this);
    }
}
