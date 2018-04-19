using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRole : InGameBaseObj {

    public int combo = 0,scores = 0;

    public BuffManager buffManager;

    TrailRenderer trail;

    private void Awake()
    {
        EventManager.Register(this,
                       EventID.EVENT_TOUCH_DOWN,
                              EventID.EVENT_TOUCH_MOVE);

        buffManager = new BuffManager();
        buffManager.Init();

        m = transform.Find("icon").GetComponent<Renderer>().material;

    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	public void RoleUpdate () {
        if (!gameObject.activeSelf) return;

 
    }
    public override void Die(){

        AudioManager.Instance.Play("sound/die");
        // game over
        InGameManager.GetInstance().GameOver();
        combo = 0;
        gameObject.SetActive(false);
        //create efffect
        GameObject effect = Resources.Load("Prefabs/Effect/RoleDieEffect") as GameObject;
        effect = Instantiate(effect);
        effect.transform.position = transform.position;

    }

    public override void HandleEvent(EventData resp)
    {

        switch (resp.eid)
        {
            case EventID.EVENT_TOUCH_DOWN:
                EventTouch eve = (EventTouch)resp;
                //TouchToPlane(eve.pos);
                //Fire(GameCommon.ScreenPositionToWorld(eve.pos));
                Vector3 pos = GameCommon.ScreenPositionToWorld(InGameManager.GetInstance().gamecamera,eve.pos);

                if(pos.x > 0){
                    KillObj();
                }else{
                    if(GetMyState() == GameObjState.red){
                        return;
                    }
                    SetState(GetMyState() == GameObjState.black ? GameObjState.white:GameObjState.black );
                }

                break;

        }

    }


    public void AddScores(int score,bool iscombo,InGameBaseObj source)
    {
        int s = score;
        if(iscombo){
            combo++;
            scores += combo;
            s = combo;
        }else {
            combo = 0;
            scores += score;
        }

        InGameManager.GetInstance().inGameUIManager.AddScores(source.transform.position, s, scores,iscombo, true);

    }

    void KillObj(){
        List<InGameBaseObj> objlist = InGameManager.GetInstance().inGameLevelManager.objList;

        int killcount = 0;
        bool iscombo = false;
        for (int i = 0; i < objlist.Count; i ++){
            InGameBaseObj obj = objlist[i];
            if(obj.myObjType == InGameBaseObj.ObjType.role){
                continue;
            }
            float dis = Mathf.Abs(transform.position.y - obj.transform.position.y);
            if(dis < obj.transform.localScale.x / 2){

                if(obj.GetMyState() != GetMyState()){
                    InGameManager.GetInstance().GameOver();
                    return;
                }

                obj.SetDie();
                killcount++;
                bool c = dis < 0.2f;
                AddScores(1, c,obj);

                if(c){
                    (new EventCreateEffect(60010014, null, obj.transform.position, 1)).Send();
                }

                iscombo = iscombo | c;

            }

        }
        if(killcount == 0){
            InGameManager.GetInstance().GameOver();
        }

        float effectscale = 2f;

        if(iscombo){
            effectscale = 6;
        }

        if(GetMyState() == GameObjState.black){
            (new EventCreateEffect(60010010, null, transform.position, effectscale)).Send();
        }else if (GetMyState() == GameObjState.white)
        {
            (new EventCreateEffect(60010011, null, transform.position, effectscale)).Send();
        }

    }

    public void Revive(){
        gameObject.SetActive(true);

    }

    private void OnDestroy()
    {
        buffManager.Destroy();
        EventManager.Remove(this);
    }
}
