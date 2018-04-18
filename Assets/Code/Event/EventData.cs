using UnityEngine;
using System.Collections;

public class EventData {
    public EventID eid;

    public EventData(EventID eid){
        this.eid = eid;
    }

	public void Send(){
		EventManager.instance().SendEvent(this);
	}

    public static EventData CreateEvent(EventID eventid){
        EventData data = new EventData(eventid);
        return data;
    }

}

public class EventEntryGame : EventData{
    public EventEntryGame() : base(EventID.EVENT_ENTRYGAME){ }
	public string uname;
	public string password;
}


public class EventChangeScene : EventData
{
    public GameSceneManager.SceneTag stag;
    public EventChangeScene(GameSceneManager.SceneTag stag) : base(EventID.EVENT_SCENE_CHANGE) {
        this.stag = stag;
    }
}

public class EventTouch : EventData
{
    public Vector3 pos { get; private set; }
    public EventTouch(EventID eid, Vector3 pos) : base(eid)
    {
        this.pos = pos;
    }
}

public class EventCreateEffect : EventData
{
    public int effectid;
    public GameObject obj;
    public Vector3 pos;
    public float scale;
    public EventCreateEffect(int effectid, GameObject obj, Vector3 pos, float scale) : base(EventID.EVENT_CREATE_EFFECT)
    {
        this.effectid = effectid;
        this.obj = obj;
        this.pos = pos;
        this.scale = scale;
    }
}