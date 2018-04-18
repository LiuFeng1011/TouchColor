using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    static MenuManager instance;
    public static MenuManager GetInstance() { return instance; }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        AudioManager.Instance.PlayBG("sound/bgm");

        Transform menu = GameObject.Find("UI Root").transform.Find("Menu");
        GameObject startBtn = menu.Find("StartGame").gameObject;
        GameUIEventListener.Get(startBtn).onClick = StartCB;

        GameObject storyBtn = menu.Find("Story").gameObject;
        GameUIEventListener.Get(storyBtn).onClick = StoryCB;

        GameObject infinityBtn = menu.Find("Infinity").gameObject;
        GameUIEventListener.Get(infinityBtn).onClick = InfinityCB;


        GameObject leaderBoardBtn = menu.Find("Anchor").Find("LeaderBoard").gameObject;
        GameUIEventListener.Get(leaderBoardBtn).onClick = ShowLB;


        GameObject noADBtn = menu.Find("Anchor").Find("NoAD").gameObject;
        GameUIEventListener.Get(noADBtn).onClick = NoADCB;

        int noad = PlayerPrefs.GetInt("noad", 0);

        if (noad == 1)
        {
            noADBtn.SetActive(false);
        }

        GameObject starBtn = menu.Find("Anchor").Find("Star").gameObject;
        GameUIEventListener.Get(starBtn).onClick = StarCB;


	}


	// Update is called once per frame
	void Update () {
        
	}

    void StoryCB(GameObject go){
        UserDataManager.selLevel = ConfigManager.storyLevelManager.GetDataList()[0];
    }

    void InfinityCB(GameObject go)
    {
        UserDataManager.selLevel = null;
    }

    void StartCB(GameObject go)
    {
        (new EventChangeScene(GameSceneManager.SceneTag.Game)).Send();
        Debug.Log(go.name);
    }

    void ShowLB(GameObject go){
        GameCenterManager.GetInstance().Showlb();
    }

    void NoADCB(GameObject go){
        PurchaseManager.GetInstance().DoIapPurchase(DoIapCB);
    }

    void DoIapCB(bool b,string s){
        
    }

    void StarCB(GameObject go){
        #if UNITY_IPHONE || UNITY_EDITOR
        const string APP_ID = "1369028254";
        var url = string.Format(
            "itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id={0}",
            APP_ID);
        Application.OpenURL(url);
        #endif
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
