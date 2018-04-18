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

                if(pos.x < 0){
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

    void KillObj(){
        List<InGameBaseObj> objlist = InGameManager.GetInstance().inGameLevelManager.objList;

        int killcount = 0;
        for (int i = 0; i < objlist.Count; i ++){
            InGameBaseObj obj = objlist[i];
            if(obj.myObjType == InGameBaseObj.ObjType.role){
                continue;
            }

            if(Mathf.Abs(transform.position.y - obj.transform.position.y) < obj.transform.localScale.x){

                obj.SetDie();
                killcount++;
            }

        }
        if(killcount == 0){
            InGameManager.GetInstance().GameOver();
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
