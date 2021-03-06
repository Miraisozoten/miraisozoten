﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class SwordComponent : MonoBehaviour
{
    PlayerStatusComponent P_Status;
    [SerializeField, Header("Hitしたオブジェクト")]
    List<GameObject> SwordHitList;

    // Start is called before the first frame update
    void Start()
    {
        P_Status = transform.root.GetComponent<PlayerStatusComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!P_Status.playerCom.IsAttack())
        {
            SwordHitList.Clear();
        }
        //if (P_Status.GetWeaponCollider())
        //{

        //}
        //else if(SwordHitList.Count>0)
        //{
        //    SwordHitList.Clear();
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("SwordHit");

        Move Movescript = other.GetComponent<Move>();

        for (int i = 0; i < SwordHitList.Count; i++)
        {
            if (SwordHitList[i].gameObject == other.gameObject)
            {
                return;
            }
        }

        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy")
        {
            SwordHitList.Add(other.gameObject);
            Movescript.HP -= (int)P_Status.AttackPower;

            if (!P_Status.playerCom.IsAttackSp() || !P_Status.playerCom.IsAttackhard())
            {
                P_Status.ExpUp();
            }

            if (Movescript.HP < 0)
            {
                //for(int i = 0; i < SwordHitList.Count; i++)
                //{
                //    if (SwordHitList[i] == other.gameObject)
                //    {
                //        return;
                //    }
                //}
                other.GetComponent<EnemyEffect>().EffectOn();
                //Destroy(other.gameObject);
                //SwordHitList.Add(other.gameObject);
            }
        }
    }

}
