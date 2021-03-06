﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerFollowCamera.cs

// プレイヤー追従カメラ
public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed = 10.0f;   // 回転速度
    [SerializeField]
    private Transform player;          // 注視対象プレイヤー

    [SerializeField]
    private float distance = 15.0f;    // 注視対象プレイヤーからカメラを離す距離

    public float CameraHeight;

    [SerializeField]
    private Quaternion vRotation;      // カメラの垂直回転(見下ろし回転)
    [SerializeField]
    public Quaternion hRotation;      // カメラの水平回転

    public float camera_x_MaxLimit = 0.5f;
    public float camera_x_MinLimit = -0.5f;

    [SerializeField, Header("ワールドの値(0.4くらいで)")]
    public float camera_w_Limit = 0.4f;

    [SerializeField, Header("マウスカメラ操作ONOFF")]
    static bool MouseComeraflag = true;

    [SerializeField, Header("プレイヤーの中心")]
    public float playerpos_y ;

    void Start()
    {
        // 回転の初期化
        vRotation = Quaternion.Euler(30, 0, 0);         // 垂直回転(X軸を軸とする回転)は、30度見下ろす回転
        hRotation = Quaternion.identity;                // 水平回転(Y軸を軸とする回転)は、無回転
        transform.rotation = hRotation * vRotation;     // 最終的なカメラの回転は、垂直回転してから水平回転する合成回転

        // 位置の初期化
        // player位置から距離distanceだけ手前に引いた位置を設定します
        transform.position = player.position - transform.rotation * Vector3.forward * distance;
    }

    void LateUpdate()
    {
        // 水平回転の更新
        //if (Input.GetMouseButton(0))
        //    hRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnSpeed, 0);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            vRotation *= Quaternion.Euler(1 * turnSpeed * -1, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vRotation *= Quaternion.Euler(-1 * turnSpeed * -1, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hRotation *= Quaternion.Euler(0, -1 * turnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            hRotation *= Quaternion.Euler(0, 1 * turnSpeed, 0);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (MouseComeraflag) MouseComeraflag = false;
            else MouseComeraflag = true;
        }
        if (MouseComeraflag)
        {
            hRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnSpeed, 0);
            vRotation *= Quaternion.Euler(Input.GetAxis("Mouse Y") * -turnSpeed / 2, 0, 0);
        }

        if (vRotation.x < camera_x_MinLimit && vRotation.w < camera_w_Limit) 
        {
            vRotation.x = camera_x_MinLimit;
            vRotation.w = camera_w_Limit;
        }
        if (vRotation.x > camera_x_MaxLimit && vRotation.w < camera_w_Limit)
        {
            vRotation.x = camera_x_MaxLimit;
            vRotation.w = camera_w_Limit;
        }


        // カメラの回転(transform.rotation)の更新
        // 方法1 : 垂直回転してから水平回転する合成回転とします
        transform.rotation = hRotation * vRotation;

        // カメラの位置(transform.position)の更新
        // player位置から距離distanceだけ手前に引いた位置を設定します(位置補正版)
        transform.position = player.position + new Vector3(0, 3, 0) - transform.rotation * Vector3.forward * distance;
        transform.position = new Vector3(transform.position.x, transform.position.y + CameraHeight, transform.position.z);

        RaycastHit hit;
        Vector3 centerPos = player.position;
        centerPos.y += playerpos_y;
        int Layer = 8;
        Layer = ~Layer;
        //Debug.Log(player.position);
        //カメラのめり込み防止
        //if(Physics.Linecast(centerPos,transform.position,out hit))
        //{
        //    if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Enemy")
        //    {
        //        Debug.Log("EnemyHitLay");
        //    }
        //    //Debug.Log(hit.collider.tag);
        //    else if (hit.collider.tag != "Player" || hit.collider.tag != "Weapon" )
        //    {
        //        transform.position = hit.point;
        //    }
        //}
    }
}