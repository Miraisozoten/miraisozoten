using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{

    public Transform goal;          //目的地となるオブジェクトのトランスフォーム格納用
    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 

    protected NavMeshAgent navMeshAgent;

    public bool chase;

    // Use this for initialization
    void Start()
    {
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<NavMeshAgent>();
        //目的地となる座標を設定する
        //agent.destination = goal.position;

        chase = false;
    }

    void OnDrawGizmos()
    {
        if (navMeshAgent && navMeshAgent.enabled)
        {
            Gizmos.color = Color.red;
            var prefPos = transform.position;

            foreach (var pos in navMeshAgent.path.corners)
            {
                Gizmos.DrawLine(prefPos, pos);
                prefPos = pos;
            }
        }
    }

    

    void Update()
    {
        //agent = GetComponent<NavMeshAgent>();
        if (chase == true)
        {
            agent.destination = goal.position;

            Debug.Log("true");
        }

        else
        {
            Debug.Log("false");
        }

        
    }

}
