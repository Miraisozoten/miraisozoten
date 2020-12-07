using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    //[SerializeField]
    //GameObject gameObject;

    //public GameObject text_1;

    public Transform PlayerTransform;

    public bool RayFlag = false;

    public bool chaseFlag = false;

    public bool LookAtFlag = false;

    [System.NonSerialized]
    public bool AvoidanceMove = false;

    //public bool MoveFlag = false;

    public bool AvoidanceFlag = false;

    [SerializeField, Header("HP")]
    public int HP;

    //[SerializeField, Header("追跡距離:最大")]
    //public float MoveDistance;

    [SerializeField, Header("移動速度:X")]
    float MoveSpeedX;

    [SerializeField, Header("移動速度:Y")]
    float MoveSpeedY;

    [SerializeField, Header("移動速度:Z")]
    float MoveSpeedZ;

    public float Distance;

    [System.NonSerialized]
    public float AvoidanceAngle;

    void Start()
    {
        //PlayerTransform.position = gameObject.transform.position;

         
    }

    void FixedUpdate()
    {
        if (chaseFlag == true)
        {
            Moveing();
        }

        if (AvoidanceFlag == true)
        {
            AvoidanceObstacle();
        }
    }

    void Moveing()
    {
        // transformを取得
        Transform myTransform = this.transform;

        if (LookAtFlag == true)
        {
            this.transform.LookAt(PlayerTransform.transform);
        }

        if (PlayerTransform.transform.position.x >= myTransform.transform.position.x + Distance|| PlayerTransform.transform.position.x >= myTransform.transform.position.x - Distance)
         {
                myTransform.Translate(MoveSpeedX, MoveSpeedY, 0.0f, Space.World);
         }

        if(PlayerTransform.transform.position.x <= myTransform.transform.position.x + Distance || PlayerTransform.transform.position.x <= myTransform.transform.position.x - Distance)
        {
                myTransform.Translate(-MoveSpeedX, MoveSpeedY, 0.0f, Space.World);
         }

        if (PlayerTransform.transform.position.z >= myTransform.transform.position.z + Distance || PlayerTransform.transform.position.z >= myTransform.transform.position.z - Distance)
        {
                myTransform.Translate(0.0f, MoveSpeedY, MoveSpeedZ, Space.World);
         }

        if (PlayerTransform.transform.position.z <= myTransform.transform.position.z + Distance || PlayerTransform.transform.position.z <= myTransform.transform.position.z - Distance)
        {
                myTransform.Translate(0.0f, MoveSpeedY, -MoveSpeedZ, Space.World);
         }
            
    }

    void AvoidanceObstacle()
    {
        Debug.Log(AvoidanceAngle+" ");

        Transform myTransform = this.transform;

        Vector3 localangle = myTransform.localEulerAngles;

        //myTransform.Rotate(0.0f, AvoidanceAngle, 0.0f);
        if (AvoidanceMove == true)
        {
            localangle.y = AvoidanceAngle;

            myTransform.localEulerAngles = localangle;

            myTransform.Translate(0.0f, 0.0f, MoveSpeedZ * 2);
        }
    }
}
