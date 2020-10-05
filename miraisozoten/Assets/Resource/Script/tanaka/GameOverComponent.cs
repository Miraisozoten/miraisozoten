using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverComponent : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーImageObj")]
    public GameObject GameoveObj;
    private Image GameoverImage;

    // Start is called before the first frame update
    void Start()
    {
        GameoverImage = GameoveObj.GetComponent<Image>();
        GameOverImageOff();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void GameOverImageOn()
    {
        GameoverImage.enabled = true;
    }
    public void GameOverImageOff()
    {
        GameoverImage.enabled = false;
    }
}
