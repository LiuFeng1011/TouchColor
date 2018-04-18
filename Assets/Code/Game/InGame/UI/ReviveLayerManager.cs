using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveLayerManager : InGameUIBaseLayer {

    float timeCount;
    int nowTime;

    GamePadScoresLabel timeLabel;

    GameObject reviveBtn, cancelBtn;

    bool showAD = false;

    public override void Init(){
        base.Init();
        reviveBtn = transform.Find("reviveBtn").gameObject;
        GameUIEventListener.Get(reviveBtn).onClick = Revive;

        cancelBtn = transform.Find("cancelBtn").gameObject;
        GameUIEventListener.Get(cancelBtn).onClick = Cancel;

        timeLabel = transform.Find("Time").GetComponent<GamePadScoresLabel>();
        timeLabel.Init(5);
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (showAD) return;
        if(timeCount <=0){
            return;
        }
        timeCount -= Time.deltaTime;
        if( timeCount <= 0){
            Cancel(null);
            return;
        }
        if((int)timeCount != nowTime){
            nowTime = (int)timeCount;
            //timeLabel.text = nowTime + "";
            timeLabel.SetScores(nowTime);
        }

	}

    void Revive(GameObject obj){
        ADManager.GetInstance().PlayReviveAD(ADCB,ADCloseCB);
        showAD = true;
        //gameObject.SetActive(false);
    }

    public void ADCloseCB(string str){
        showAD = false;
    }

    public void ADCB(string str){
        InGameManager.GetInstance().Revive();
        InGameManager.GetInstance().inGameUIManager.Revive();
        Hide();
    }

    void Cancel(GameObject obj){
        InGameManager.GetInstance().inGameUIManager.ShowResultLayer();
        //gameObject.SetActive(false);
        Hide();
    }

    public override void Show(){
        base.Show();
        timeCount = 5.0f;
        nowTime = 5;
    }

}
