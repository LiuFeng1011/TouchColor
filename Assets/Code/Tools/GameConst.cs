using UnityEngine;
using System.IO;

public enum enMSObjID{
	en_obj_id_null		=	0,
	en_obj_id_obj_1		= 	1001,
	en_obj_id_obj_2,
}

//阵营
public enum enMSCamp{
	en_camp_player,//玩家
	en_camp_enemy,//敌人
	en_camp_neutral,//中立
	en_camp_other,//其他
}

public enum enDataType{
	en_datatype_int,
	en_datatype_float,
	en_datatype_string,
	en_datatype_end = 1000,
}

//角色动作
public enum enObjectAnimationType{
	en_animationtype_wait,//待机
	en_animationtype_run,//跑
	en_animationtype_jump,//跳
	en_animationtype_atk1,//攻击
	en_animationtype_atk2,
	en_animationtype_atk3,
	en_animationtype_atk4,
	en_animationtype_atk5,
}

//刷怪点类型
public enum enEnemyPointType{
	en_enemypoint_type_null,
	en_enemypoint_type_dailytiem,//时间间隔
	en_enemypoint_type_event,//

}

//AI Type
public enum enAIType{
	en_aitype_wait,//木桩
	en_aitype_counterattack,//反击
	en_aitype_active,//主动
	en_aitype_patrol,//巡逻
}

public enum enGameState{
    ready,
    playing,
    pause,
    over
}

public struct GameModel{
    public int modelid;
    public string name;
    public string lbname;

    public GameModel(int modelid,string name, string lbname){
        this.modelid = modelid;
        this.name = name;
        this.lbname = lbname;
    }
}

public static class GameConst  {
	
	public const string userDataFileName = "userdata";
    public const string CONF_FILE_NAME = ".bytes";

    public const string USERDATANAME_MODEL = "model";
    public const string USERDATANAME_MODEL_MAXSCORES = "model_maxscores_";
    public const string USERDATANAME_MODEL_LASTSCORES = "model_lastscores_";

    public const float JUMP_FORCE = 3f;

    public static GameModel[] gameModels = {
        //new GameModel(0,"LEVEL","ToTarget_Normal_Leaderboard"),
        //new GameModel(1,"INFINITY","ToTarget_Speed_Leaderboard"),
    };


    public const int MAP_WIDTH = 8;
    public const float MAP_OBJ_MAX_POSX = 3;

    public const float comboDis = 0.3f;
    public const int timeModelTime = 60;

    public const float STEP_MIN_SIZE = 1f;
    public const float STEP_MAX_SIZE = 2.5f;

	
    public static string GetLevelDataFilePath(string filename)
    {
        if (!Directory.Exists(Application.streamingAssetsPath + "/LevelData"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/LevelData");//创建文件夹

        }
        return Application.streamingAssetsPath + "/LevelData/" + filename;
    }
    public static string GetLevelDataFilePath2(string tablename)
    {
        string src = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            src = "jar:file://" + Application.dataPath + "!/assets/LevelData/" + tablename;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            src = "file://" + Application.dataPath + "/Raw/LevelData/" + tablename;
        }
        else
        {
            src = "file://" + Application.streamingAssetsPath + "/LevelData/" + tablename;
        }
        return src;
    }
	public static string GetExcelFilePath(string filename){
		return Application.dataPath+"/ExcelTools/xlsx/"+filename;
	}

	public static string GetConfigFilePath(string tablename){
		string src = "";

		if (Application.platform == RuntimePlatform.Android)
		{
			src = "jar:file://" + Application.dataPath + "!/assets/Config/" + tablename;
		}else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			src = "file://" + Application.dataPath + "/Raw/Config/" + tablename;
		}else{
			src = "file://" + Application.streamingAssetsPath + "/Config/" + tablename;
		}
		return src;
	}
	public static string GetConfigPath(){
		return "Config/";
	}

	public static string SaveConfigFilePath(string tablename)
    {
        return "Assets/Resources/Config/" + tablename;
	}

	public static string GetPersistentDataPath(string filename){
		return Application.persistentDataPath + "/" + filename;  
	}

	public static string[] objectAnimationName = {"wait","run","jump","atk1","atk2","atk3","atk4","atk5"};
}
