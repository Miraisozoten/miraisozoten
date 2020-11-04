using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayermanagerComponent : MonoBehaviour
{
    [SerializeField, Header("PlayerObj")]
    public GameObject PlayerObj;
    private PlayerStatusComponent PlayerStatusCom;
    [SerializeField, Header("GameOverObj")]
    public GameObject GameoverObj;
    private GameOverComponent GameoverCom;
    [SerializeField, Header("GameClearObj")]
    public GameObject GameclearObj;
    private GameClearComponent GameclearCom;

    [SerializeField, Header("SceneManager")]
    public GameObject SceneManager;
    private SceneManage sceneManagerCom;

    [SerializeField, Header("敵カウント")]
    public int enemycount = 10;
    private bool gameoverFlag;
    private bool gameclearFlag;
    private bool resetFlag;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStatusCom = PlayerObj.GetComponent<PlayerStatusComponent>();
        GameoverCom = GameoverObj.GetComponent<GameOverComponent>();
        GameclearCom = GameclearObj.GetComponent<GameClearComponent>();
        sceneManagerCom = SceneManager.GetComponent<SceneManage>();

        gameoverFlag = false;
        gameclearFlag = false;
        resetFlag = false;

        //enemycount = 10;

        enemycount = GameObject.FindGameObjectsWithTag("Boss").Length;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q) && (gameoverFlag || gameclearFlag)) 
        {
            GameoverCom.GameOverImageOff();
            sceneManagerCom.Reset();
        }
        //if (Input.GetKey(KeyCode.Space) && gameclearFlag)
        //{
        //    GameclearCom.GameclrImageOff();
        //    sceneManagerCom.Reset();
        //}


        if ((PlayerStatusCom.HP_GaugeSet() || PlayerStatusCom.HP_IconSet()) && !gameclearFlag)
        {
            GameOver();
        }
        else if (enemycount == 0 && !gameoverFlag) 
        {
            GameClear();
        }
    }

    void GameOver()
    {
        gameoverFlag = true;
        GameoverCom.GameOverImageOn();
    }

    void GameClear()
    {
        gameclearFlag = true;
        sceneManagerCom.GameClear();
        GameclearCom.GameclrImageOn();
    }

    //現在のエキス
    public int NowExtraExtractPointct()
    {
        return PlayerStatusCom.ExpNow();
    }

    //エキス上昇
    public void ExtractPointUp()
    {
        PlayerStatusCom.ExpUp();
    }
    //エキス減少
    public void ExtractPointDown()
    {
        PlayerStatusCom.ExpDown();
    }

    public void EnemyCountSet()
    {

    }
}
