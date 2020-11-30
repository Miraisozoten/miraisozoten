using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    //[SerializeField]
    //GameObject gameObject;

    public GameObject text_1;

    public Transform PlayerTransform;

    public bool RayFlag = false;

    //public bool MoveFlag = false;

    public bool AvoidanceFlag = false;

    [SerializeField, Header("HP")]
    public int HP;

    [SerializeField, Header("追跡距離:最大")]
    public float MoveDistance;

    [SerializeField, Header("移動速度:X")]
    float MoveSpeedX;

    [SerializeField, Header("移動速度:Y")]
    float MoveSpeedY;

    [SerializeField, Header("移動速度:Z")]
    float MoveSpeedZ;

    public bool chaseFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerTransform.position = gameObject.transform.position;

         
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

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

        Text HP_Text = text_1.GetComponent<Text>();

        HP_Text.text = "HP:"+ HP;

        //Debug.Log(PlayerTransform.transform.position.x);
    }

    void Moveing()
    {
        // transformを取得
        Transform myTransform = this.transform;

        this.transform.LookAt(PlayerTransform.transform);

        if (PlayerTransform.transform.position.x >= myTransform.transform.position.x)
            {
                //Debug.Log("A");
                myTransform.Translate(MoveSpeedX, MoveSpeedY, 0.0f, Space.World);
            }

            else
            {
                //Debug.Log("B");
                myTransform.Translate(-MoveSpeedX, MoveSpeedY, 0.0f, Space.World);
            }

            if (PlayerTransform.transform.position.z >= myTransform.transform.position.z)
            {
                //Debug.Log("C");
                myTransform.Translate(0.0f, MoveSpeedY, MoveSpeedZ, Space.World);
            }

            else
            {
                //Debug.Log("D");
                myTransform.Translate(0.0f, MoveSpeedY, -MoveSpeedZ, Space.World);
            }
            
    }

    void AvoidanceObstacle()
    {
        Transform myTransform = this.transform;

        myTransform.Rotate(0.0f, 0.0f, 0.0f);

        myTransform.Translate(0.0f, 0.0f, 0.0f,Space.World);
    }
}
