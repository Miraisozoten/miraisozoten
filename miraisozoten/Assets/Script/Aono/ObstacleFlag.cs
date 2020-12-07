using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFlag : MonoBehaviour
{

    [SerializeField]
    Move script;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {   //RayのRotationの初期化

            Transform myTransform = this.transform;

            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.x = 0.0f;
            localAngle.y = 0.0f;
            localAngle.z = 0.0f;
            myTransform.localEulerAngles = localAngle;
        }
    }

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
