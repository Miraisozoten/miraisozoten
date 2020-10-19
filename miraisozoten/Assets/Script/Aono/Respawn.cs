using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField,HeaderAttribute("リスポーンさせる敵")]

    public GameObject OriginObject;

    public int EnemyNum;

    private float TImer = 0.0f;

    private float Interval = 2.0f;

    private float ResTime = 10.0f;

    GameObject[] tagObjects;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(OriginObject, new Vector3(30.0f, -10.0f, 100.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        TImer += Time.time;

        if (TImer > Interval)
        {
            Check("Enemy");
            TImer = 0;
        }


        if (Time.time >=ResTime)
        {
            Debug.Log("OK");
            ResTime += Time.time+90.0f;
            //今の時間に＋する処理
        }
    }

    void Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(tagObjects.Length);
        if (tagObjects.Length <= EnemyNum)
        {
            Instantiate(OriginObject, new Vector3(30.0f, -10.0f, 100.0f), Quaternion.identity);
        }
    }
}
