using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearComponent : MonoBehaviour
{
    [SerializeField, Header("ゲームクリアImageObj")]
    public GameObject GameclrObj;
    private Image GameclrImage;

    // Start is called before the first frame update
    void Start()
    {
        GameclrImage = GameclrObj.GetComponent<Image>();
        GameclrImageOff();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void GameclrImageOn()
    {
        GameclrImage.enabled = true;
    }
    public void GameclrImageOff()
    {
        GameclrImage.enabled = false;
    }
}
