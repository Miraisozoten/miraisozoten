using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchObstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
