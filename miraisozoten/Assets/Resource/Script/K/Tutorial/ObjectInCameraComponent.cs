using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInCameraComponent : MonoBehaviour
{
    bool InCamera = false;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InCamera = false;
    }

    private void OnBecameVisible()
    {
        InCamera = true;
    }
    public bool GetInCameraFlag()
    {
        return InCamera;
    }
}
