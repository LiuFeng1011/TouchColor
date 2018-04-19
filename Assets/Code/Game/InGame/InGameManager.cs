using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {
    static InGameManager instance;

    public InGameRole role;

    GameTouchController gameTouchController;
    public InGameLevelManager inGameLevelManager;
    public InGameUIManager inGameUIManager;
    public InGameBgColor inGameBgColor;

    public GameEffectManager gameEffectManager;

    public RapidBlurEffectManager rapidBlurEffectManager;

    public Camera gamecamera;

    int reviveCount = 0;

    InGameBaseModel modelManager;


    public MapData md = new MapData();


    enGameState gameState;

    public static float gameTime = 0f;

    Rect gameRect;

    [HideInInspector] public float gameSpeed = 3,gameScale = 0f, maxSpeed = 5,aloneGameTime = 0,maxSpeedTime = 180;

    public static InGameManager GetInstance(){
        return instance;
    }

    private void Start()
    {
        instance = this;
        gamecamera = Camera.main;

        rapidBlurEffectManager = gamecamera.gameObject.AddComponent<RapidBlurEffectManager>();


        Vector3 screenLeftDown = new Vector3(0,0, 0);
        Vector3 screenRightTop = new Vector3(Screen.width, Screen.height, 0);

        Vector3 gameLeftDown = GameCommon.ScreenPositionToWorld(gamecamera, screenLeftDown);
        Vector3 gameRightTop = GameCommon.ScreenPositionToWorld(gamecamera, screenRightTop);

        gameRect = new Rect(gameLeftDown, gameRightTop - gameLeftDown);

        gameState = enGameState.ready;

        if(UserDataManager.selLevel == null){
            InitGame();
        }else{
            StartCoroutine(ReadConfigFile(UserDataManager.selLevel.file_path));
        }
       // 

    }

    // Use this for initialization
    void InitGame () {
        gameTouchController = new GameTouchController();

        //创建角色
        GameObject roleObj = Resources.Load("Prefabs/MapObj/InGameRole") as GameObject;
        roleObj = Instantiate(roleObj);
        role = roleObj.GetComponent<InGameRole>();

        roleObj.transform.position = new Vector3(0,GetGameRect().y + 3,0);

        gameEffectManager = new GameEffectManager();

        inGameBgColor = new InGameBgColor();
        inGameBgColor.Init();
        //
        inGameLevelManager = new InGameLevelManager();
        inGameLevelManager.Init();

        inGameUIManager = new InGameUIManager();
        inGameUIManager.Init();

        int selmodel = PlayerPrefs.GetInt(GameConst.USERDATANAME_MODEL, 0);
        modelManager = InGameBaseModel.Create(selmodel);
        modelManager.Init();

        gameState = enGameState.playing;

    }
	
	// Update is called once per frame
	void Update () {
        gameTime += Time.deltaTime;
        if (inGameUIManager != null) inGameUIManager.Update();

        if(gameState != enGameState.playing){
            return;
        }

        aloneGameTime += Time.deltaTime;

        gameScale = Mathf.Min(1, aloneGameTime / maxSpeedTime);
        gameSpeed = 3 + maxSpeed * gameScale;

        if (gameTouchController != null) gameTouchController.Update();
        if (inGameLevelManager != null)inGameLevelManager.Update();
        if (modelManager != null) modelManager.Update();
        if (inGameBgColor != null) inGameBgColor.Update();

        if (role != null) role.RoleUpdate();
	}

    private void OnDestroy()
    {
        instance = null;
        if (inGameLevelManager != null) inGameLevelManager.Destroy();
        if (inGameUIManager != null) inGameUIManager.Destroy();
        if (modelManager != null) modelManager.Destroy();
        if (inGameBgColor != null) inGameBgColor.Destroy();
        if (gameEffectManager != null) gameEffectManager.Destroy();

    }

    public void GameWin(){
        rapidBlurEffectManager.StartBlur();
        Invoke("ShowWinLayer", 1.0f);
    }

    public void ShowWinLayer(){
        gameState = enGameState.over;
        inGameUIManager.ShowWinLayer();
    }

    public void GameOver(){
        gameState = enGameState.over;

        rapidBlurEffectManager.StartBlur();
        Invoke("ShowOverLayer", 1.0f);
    }

    public void ShowOverLayer(){

        gameState = enGameState.over;

        int selmodel = PlayerPrefs.GetInt(GameConst.USERDATANAME_MODEL, 0);
        int basescores = PlayerPrefs.GetInt(GameConst.USERDATANAME_MODEL_MAXSCORES + selmodel);

        int thisscores = role.scores;
        if (basescores < thisscores)
        {
            PlayerPrefs.SetInt(GameConst.USERDATANAME_MODEL_MAXSCORES + selmodel, thisscores);

            GameCenterManager.GetInstance().UploadScores(GameConst.gameModels[selmodel].lbname,thisscores);
        }
        PlayerPrefs.SetInt(GameConst.USERDATANAME_MODEL_LASTSCORES + selmodel, thisscores);

        if (reviveCount <= 0 && ADManager.GetInstance().isAdLoaded)
        {
            inGameUIManager.ShowReviveLayer();
        }
        else
        {
            inGameUIManager.ShowResultLayer();

        }
        reviveCount += 1;
    }

    public void Pause(){
        gameState = enGameState.pause;
    }

    public void Resume(){
        gameState = enGameState.playing;
    }

    public void Revive(){

        gameState = enGameState.playing;

        rapidBlurEffectManager.OverBlur();
        role.Revive();
        modelManager.Revive();
    }

    public void Restart(){
        reviveCount = 0;

    }

    public Rect GetGameRect(){
        return new Rect(gameRect.x, gameRect.y + gamecamera.transform.position.y, gameRect.width, gameRect.height); 

    }
    IEnumerator ReadConfigFile(string filename)
    {
        //GameConst.GetStoryLevelFilePath(mapid+".lmd")
        string filepath;//= GameConst.GetConfigFilePath(filename);

        filepath = GameConst.GetLevelDataFilePath2(filename);
        Debug.Log(filepath);

        filepath += ".lmd";
        WWW www = new WWW(filepath);
        yield return www;
        while (www.isDone == false) yield return null;
        if (www.error == null)
        {
            byte[] gzipdata = www.bytes;
            byte[] levelData = GameCommon.UnGZip(gzipdata);
            DataStream datastream = new DataStream(levelData, true);
            md.Deserialize(datastream);
            InitGame();
        }
        else
        {
            //GameLogTools.SetText("wwwError<<" + www.error + "<<" + filepath);
            Debug.Log("wwwError<<" + www.error + "<<" + filepath);
            (new EventChangeScene(GameSceneManager.SceneTag.Menu)).Send();
        }

    }
}
