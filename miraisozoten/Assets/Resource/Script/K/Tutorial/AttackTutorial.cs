using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATTACK_STATE
{
    LIGHT_ATTACK,
    STRONG_ATTACK,
    JUICE_ACTION_SELECT,
    JUICE_ACTION,
};

public class AttackTutorial:Tutorial
{
    private ATTACK_STATE AttackState = ATTACK_STATE.LIGHT_ATTACK;

    public GameObject img;
    public GameObject img2;
    public GameObject img3;
    public GameObject img4;
    
    public KeyCode EssenseAttackKey;

    public float MouseScrol;
    private float BeforeMouseScroll;
    // Start is called before the first frame update
    override protected void Start()
    {
        //obj1 = CreateObject(obj1);
        //エラーログ
        if (img==null)
            Debug.LogError("imgの値がnullです。");
        if(img2==null)
            Debug.LogError("img2の値がnullです。");

        img = CreateUIObject(img);
    }

    // Update is called once per frame
    override protected void Update()
    {
        LightAttackTutorial();
        StrongAttackTutorial();
        EssenseSelectTutorial();
        EssenseAttackTutorial();
    }

    void LightAttackTutorial()
    {
        if (img != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(img.gameObject);
                img2 = CreateUIObject(img2);
            }
        }
    }

    void StrongAttackTutorial()
    {
        if (img2 != null && img == null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(img2.gameObject);
                img3 = CreateUIObject(img3);
            }
        }
    }

    void EssenseSelectTutorial()
    {
        if (img3 != null && img2 == null)
        {
            MouseScrol = Input.GetAxis("Mouse ScrollWheel");
            if (MouseScrol!=BeforeMouseScroll)
            {
                Destroy(img3.gameObject);
                img4 = CreateUIObject(img4);
            }
            BeforeMouseScroll = MouseScrol;
        }
    }

    void EssenseAttackTutorial()
    {
        if (img4 != null && img3 == null)
        {
            if (Input.GetKeyDown(EssenseAttackKey))
            {
                Destroy(img4.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}