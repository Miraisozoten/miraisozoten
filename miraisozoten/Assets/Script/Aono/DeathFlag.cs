using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {

        Debug.Log("a");

        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
