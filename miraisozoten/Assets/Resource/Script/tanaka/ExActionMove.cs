using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExActionMove : MonoBehaviour
{
    public List<Canvas> ActionList;
    int actionsort;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("heal", true);

        for(int i = 0; i < transform.childCount; i++)
        {
            ActionList.Add(gameObject.transform.GetChild(i).GetComponent<Canvas>());
        }

        Debug.Log(ActionList[0].sortingOrder);
        ActionList[0].sortingOrder = 1;
        ActionList[1].sortingOrder = 0;
        ActionList[2].sortingOrder = 1;
        actionsort = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {
    }

    public void SetUIDown()
    {
        for(int i = 0; i < ActionList.Count; i++)
        {
            ActionList[i].sortingOrder += 1;
            if (ActionList[i].sortingOrder > 2)
            {
                ActionList[i].sortingOrder = 0;
            }
        }
    }
    public void SetUIUp()
    {

    }

    //回復トリガー
    public void IsHeal()
    {
        anim.SetBool("heal", true);
        anim.SetBool("Special", false);
        anim.SetBool("Clairvoyance", false);

        if (anim.GetBool("pos"))
        {
            ActionList[0].sortingOrder = 2;
            ActionList[1].sortingOrder = 0;
            ActionList[2].sortingOrder = 1;
        }
        else
        {
            ActionList[0].sortingOrder = 2;
            ActionList[1].sortingOrder = 1;
            ActionList[2].sortingOrder = 0;
        }
    }
    //必殺トリガー
    public void IsSpecial()
    {
        anim.SetBool("heal", false);
        anim.SetBool("Special", true);
        anim.SetBool("Clairvoyance", false);

        if (anim.GetBool("pos"))
        {
            ActionList[0].sortingOrder = 1;
            ActionList[1].sortingOrder = 2;
            ActionList[2].sortingOrder = 0;
        }
        else
        {
            ActionList[0].sortingOrder = 0;
            ActionList[1].sortingOrder = 2;
            ActionList[2].sortingOrder = 1;
        }

    }
    //千里トリガー
    public void IsCleirvoyance()
    {
        anim.SetBool("heal", false);
        anim.SetBool("Special", false);
        anim.SetBool("Clairvoyance", true);

        if (anim.GetBool("pos"))
        {
            ActionList[0].sortingOrder = 0;
            ActionList[1].sortingOrder = 1;
            ActionList[2].sortingOrder = 2;
        }
        else
        {
            ActionList[0].sortingOrder = 1;
            ActionList[1].sortingOrder = 0;
            ActionList[2].sortingOrder = 2;
        }

    }

    //上
    public void WheelUp()
    {
        anim.SetBool("pos", true);
    }
    //下
    public void WheelDown()
    {
        anim.SetBool("pos", false);
    }

    public bool GetTransition()
    {
        //アニメーション中ではない=true
        return !anim.IsInTransition(0);
    }
}