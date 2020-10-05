using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField, Header("リセットするとき")]
    public bool ResetFlag;
    [SerializeField, Header("クリアしたとき")]
    public bool ClearFlag;
    void Start()
    {
        ResetFlag = false;
        ClearFlag = false;
    }

    void FixedUpdate()
    {
        if (ResetFlag)
        {
            Reset();
        }
    }

   public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Stage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void Stage3()
    {
        SceneManager.LoadScene("Stage3");
    }

    public void Stage4()
    {
        SceneManager.LoadScene("Stage4");
    }

    public void Stage5()
    {
        SceneManager.LoadScene("Stage5");

    }

    public void StageSelect()
    {
        SceneManager.LoadScene("Stage Select");
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Result()
    {
        SceneManager.LoadScene("Result");
    }

    public void GameClear()
    {
        ClearFlag = true;
    }

    public void Reset()
    {
        ResetFlag = false;
        ClearFlag = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
