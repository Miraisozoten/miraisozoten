using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlag : MonoBehaviour
{
    [SerializeField]
    NavTest script;

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            script.chase = false;
            Debug.Log("攻撃中");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            script.chase = true;
        }
    }
}
