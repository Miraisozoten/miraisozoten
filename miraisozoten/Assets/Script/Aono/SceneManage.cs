using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
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
}
