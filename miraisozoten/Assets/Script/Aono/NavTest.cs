using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{

    public Transform goal;          //目的地となるオブジェクトのトランスフォーム格納用
    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 

    // Use this for initialization
    void Start()
    {
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<NavMeshAgent>();
        //目的地となる座標を設定する
        agent.destination = goal.position;
    }

}
