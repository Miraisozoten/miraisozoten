using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField, HeaderAttribute("リスポーン敵1")]
    public GameObject OriginObject;

   // public string EnemyTag1;

    [SerializeField, HeaderAttribute("リスポーン敵1の数")]
    public int EnemyNum;

    //[SerializeField, HeaderAttribute("リスポーンX座標")]
    public float RespawnX_1;

    //[SerializeField, HeaderAttribute("リスポーンY座標")]
    public float RespawnY_1;

    //[SerializeField, HeaderAttribute("リスポーン座標")]
    public float RespawnZ_1;

    [SerializeField, HeaderAttribute("リスポーン敵2")]
    public GameObject OriginObject2;

    //public string EnemyTag2;

    [SerializeField, HeaderAttribute("リスポーン敵2の数")]
    public int EnemyNum2;

    //[SerializeField, HeaderAttribute("リスポーンX座標")]
    public float RespawnX_2;

    //[SerializeField, HeaderAttribute("リスポーンY座標")]
    public float RespawnY_2;

    //[SerializeField, HeaderAttribute("リスポーン座標")]
    public float RespawnZ_2;

    [SerializeField, HeaderAttribute("リスポーン敵3")]
    public GameObject OriginObject3;

    //public string EnemyTag3;

    [SerializeField, HeaderAttribute("リスポーン敵3の数")]
    public int EnemyNum3;

    //[SerializeField, HeaderAttribute("リスポーンX座標")]
    public float RespawnX_3;

    //[SerializeField, HeaderAttribute("リスポーンY座標")]
    public float RespawnY_3;

    //[SerializeField, HeaderAttribute("リスポーン座標")]
    public float RespawnZ_3;

    [SerializeField, HeaderAttribute("リスポーン敵4")]
    public GameObject OriginObject4;

    //public string EnemyTag4;

    [SerializeField, HeaderAttribute("リスポーン敵4の数")]
    public int EnemyNum4;

    //[SerializeField, HeaderAttribute("リスポーンX座標")]
    public float RespawnX_4;

    //[SerializeField, HeaderAttribute("リスポーンY座標")]
    public float RespawnY_4;

    //[SerializeField, HeaderAttribute("リスポーン座標")]
    public float RespawnZ_4;

    string EnemyName;

    string EnemyName2;

    //[SerializeField, HeaderAttribute("リスポーンX座標")]
    //public float RespawnX;

    //[SerializeField, HeaderAttribute("リスポーンY座標")]
    //public float RespawnY;

    //[SerializeField, HeaderAttribute("リスポーン座標")]
    //public float RespawnZ;

    private float RandomNum_X;
    private float RondomNum_Z;

    [SerializeField, HeaderAttribute("リスポーン座標の振れ幅の上")]
    public float RandamRange_Tops;

    [SerializeField, HeaderAttribute("リスポーン座標の振れ幅の下")]
    public float RandamRange_Bottom;

    private float TImer = 0.0f;

    private float Interval = 2.0f;

    private float ResTime = 10.0f;

    GameObject[] tagObjects;
    GameObject[] tagObjects2;
    GameObject[] tagObjects3;
    GameObject[] tagObjects4;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(OriginObject, new Vector3(RespawnX_1, RespawnY_1, RespawnZ_1), Quaternion.identity);

        //Instantiate(OriginObject2, new Vector3(RespawnX_2, RespawnY_2, RespawnZ_2), Quaternion.identity);

        //EnemyName = OriginObject.name;

        //EnemyName2 = OriginObject2.name;

        //Debug.Log(EnemyName + "(Clone)");
        //Debug.Log(EnemyName2);
    }

    // Update is called once per frame
    void Update()
    {
        TImer += Time.time;

        RandomNum_X = Random.Range(RandamRange_Bottom, RandamRange_Tops);

        Random.InitState(System.DateTime.Now.Millisecond);

        RondomNum_Z = Random.Range(RandamRange_Bottom, RandamRange_Tops);

        Random.InitState(System.DateTime.Now.Millisecond);

        if (TImer > Interval)
        {
            Check(OriginObject.tag);
            TImer = 0;
        }


        if (Time.time >=ResTime)
        {
            //Debug.Log("OK");
            ResTime += Time.time+90.0f;
            //今の時間に＋する処理
        }
    }

    void Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(OriginObject.tag);
        tagObjects2 = GameObject.FindGameObjectsWithTag(OriginObject2.tag);
        tagObjects3 = GameObject.FindGameObjectsWithTag(OriginObject3.tag);
        tagObjects4 = GameObject.FindGameObjectsWithTag(OriginObject4.tag);
        //Debug.Log(tagObjects.Length);
        if (tagObjects.Length < EnemyNum)
        {
            Instantiate(OriginObject, new Vector3(RespawnX_1+RandomNum_X, RespawnY_1, RespawnZ_1+ RondomNum_Z), Quaternion.identity);
        }

        if (tagObjects2.Length <= EnemyNum2)
        {
            Instantiate(OriginObject2, new Vector3(RespawnX_2 + RandomNum_X, RespawnY_2, RespawnZ_2 + RondomNum_Z), Quaternion.identity);
        }

        if (tagObjects3.Length <= EnemyNum3)
        {
            Instantiate(OriginObject3, new Vector3(RespawnX_3 + RandomNum_X, RespawnY_3, RespawnZ_3 + RondomNum_Z), Quaternion.identity);
        }

        if (tagObjects4.Length <= EnemyNum4)
        {
            Instantiate(OriginObject4, new Vector3(RespawnX_4 + RandomNum_X, RespawnY_4, RespawnZ_4 + RondomNum_Z), Quaternion.identity);
        }
    }
}
