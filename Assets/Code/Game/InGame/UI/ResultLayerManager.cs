using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLayerManager : InGameUIBaseLayer {

    public UILabel scoreLabel,bestLabel;

    public static int playCount = 0;
    public static float lastPlayerTime = 0;

    public override void Init(){
        base.Init();
        GameObject exitbtn = transform.Find("exit").gameObject;
        GameUIEventListener.Get(exitbtn).onClick = ExitBtn;

        GameObject replaybtn = transform.Find("replay").gameObject;
        GameUIEventListener.Get(replaybtn).onClick = ReplayBtn;

        scoreLabel = transform.Find("scores").GetComponent<UILabel>();


        bestLabel = transform.Find("Best").GetComponent<UILabel>();
    }

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Show(){
        base.Show();
        gameObject.SetActive(true);

        playCount++;


        int selmodel = PlayerPrefs.GetInt(GameConst.USERDATANAME_MODEL, 0);
        int bestscores = PlayerPrefs.GetInt(GameConst.USERDATANAME_MODEL_MAXSCORES + selmodel);
        bestLabel.text = "Best:" + bestscores;


        if(InGameManager.gameTime - lastPlayerTime > 30 && playCount > 3 && ADManager.GetInstance().isAdLoaded){
            playCount = 0;
            lastPlayerTime = InGameManager.gameTime;
            ADManager.GetInstance().PlayGameOverAD();
        }
    }

    public void SetVal(int val){

        scoreLabel.text = val + "";
    }

    void ExitBtn(GameObject obj){
        (new EventChangeScene(GameSceneManager.SceneTag.Menu)).Send();
        gameObject.SetActive(false);
    }

    void ReplayBtn(GameObject obj){
        (new EventChangeScene(GameSceneManager.SceneTag.Game)).Send();
        gameObject.SetActive(false);
    }
}
