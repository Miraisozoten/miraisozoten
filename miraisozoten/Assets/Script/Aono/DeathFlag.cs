using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlag : MonoBehaviour
{
    [SerializeField]
    CountDown script;

    [SerializeField, Header("HP")]
    public int HitPoint;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Boss")
            {
               script.Hoge();   
            }

           // Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
