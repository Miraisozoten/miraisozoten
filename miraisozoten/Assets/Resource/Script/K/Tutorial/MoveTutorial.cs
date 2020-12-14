using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorial : Tutorial
{
    //インスペクター側で設定
    public GameObject img;
    public GameObject img2;

    public KeyCode avoidance;

    // Start is called before the first frame update
    override protected void Start()
    {
        //エラーログ
        if (img == null)
            Debug.LogError("imgの値がnullです。");
        if (img2 == null)
            Debug.LogError("img2の値がnullです。");
        //imgの作成
        img = CreateUIObject(img);
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (img == null && img2 != null)
        {
            GetAvoidanceKey();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            DestroyObject(img);

            img2 = CreateUIObject(img2);
        }
    }

    void GetAvoidanceKey()
    {
        if (Input.GetKeyDown(avoidance))
        {
            DestroyObject(img2);
            Destroy(this.gameObject);
        }
    }
}