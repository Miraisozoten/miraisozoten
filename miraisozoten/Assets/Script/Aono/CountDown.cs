using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    SceneManage script;

    [SerializeField]
    int StageNum;

    bool Flag = false;

    //public Text timeText;

    public float ClearTime;

    public Image image;
    private Sprite sprite;

    GameObject game;



    void Start()
    {
        game = GameObject.Find("SceneManager");

        Flag = script.GetComponent<SceneManage>().ClearFlag;
    }


    public void GoResult()
    {

        //script.Result1();

        //if (StageNum == 1)
        //{
        //    script.Result1();
        //}

        switch (StageNum)
        {
            case 1:
                script.Result1();
                break;

            case 2:
                script.Result2();
                break;

            case 3:
                script.Result3();
                break;

            case 4:
                script.Result4();
                break;

            case 5:
                script.Result5();
                break;

            default:
                Debug.Log("StageNum未設定");
                break;
        }
    }

    public void Hoge()
    {
        Flag = true;
        Invoke("GoResult", 3);
    }

    // Update is called once per frame
    void Update()
    {
        //bool Flag = script.GetComponent<SceneManage>().ClearFlag;


        if (Flag == false)
        {
            //timeText.text = "Please Space";
        }

        if (Flag == true)
        {
            ClearTime += Time.deltaTime;

            //timeText.text = ClearTime.ToString("f1") + "秒";

            sprite = Resources.Load<Sprite>("ramen");
            image = image.GetComponent<Image>();
            image.sprite = sprite;
        }

        

        if (ClearTime >= 5.0f)
        {
            //timeText.text = "Clear";

            game.GetComponent<SceneManage>().Result1();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Flag = true;

            sprite = Resources.Load<Sprite>("ramen");
            image = image.GetComponent<Image>();
            image.sprite = sprite;
        }

    }
}
