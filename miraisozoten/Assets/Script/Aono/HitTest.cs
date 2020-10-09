using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTest : MonoBehaviour
{
     [SerializeField]
    NavTest script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Weapon")
        {
            Debug.Log("攻撃が当たった");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
