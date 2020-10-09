using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAITest : MonoBehaviour
{
    //ここらへんに列挙型で行動表示


    //ここらに変数


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("プレイヤー検知");
        }
    }


    // Update is called once per frame
    void Update()
    {
        //ここらで敵の行動分岐だったり
    }
}
