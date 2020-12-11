using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseFlag : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Move script;

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            script.chaseFlag = true;
            //Debug.Log("追跡中");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
           script.chaseFlag = false;
           //Debug.Log("見失った");
        }
    }
}
