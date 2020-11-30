using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFlag : MonoBehaviour
{

    [SerializeField]
    Move script;

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Obstacle")
        {
            script.AvoidanceFlag = true;
            script.chaseFlag = false;
            //Debug.Log("追跡中");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Obstacle")
        {
            script.AvoidanceFlag = false;
            //Debug.Log("見失った");
        }
    }
}
