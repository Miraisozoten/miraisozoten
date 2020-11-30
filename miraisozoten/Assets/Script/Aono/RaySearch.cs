﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySearch : MonoBehaviour
{
    [SerializeField]
    Move script;

    public float RayDistance;

    public float RaySpeed;

    public float RayMargin;

    float RayOrigin;

    public bool ReverseFlag;

    //public bool RayFlag = false;

    Vector3 fwd;

    // Ray ray = new Ray(transform.position, transform.forward);

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        script.RayFlag = true;
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(RayOrigin + "m");

        if(script.RayFlag == true)
        {

            if (other.tag == "Obstacle")
            {

                Transform mytransform = this.transform;

                Vector3 localAngle = mytransform.localEulerAngles;

                if (ReverseFlag == false)
                {
                    localAngle.y += RaySpeed;
                }

                else
                {
                    localAngle.y -= RaySpeed;
                }

                mytransform.localEulerAngles = localAngle;

                if (Physics.Raycast(transform.position, fwd, RayDistance))
                {
                    //print("There is something in front of the object!");

                    RayOrigin = localAngle.y;
                }

                else
                {
                    print(localAngle.y + "m");

                    if (ReverseFlag == true)
                    {
                        if (RayOrigin >= localAngle.y + RayMargin)
                        {
                            script.RayFlag = false;
                        }
                    }

                    else
                    {
                        if (RayOrigin+RayMargin <=localAngle.y)
                        {
                            script.RayFlag = false;
                        }

                    }

                    //script.RayFlag = false;
                }

                Debug.DrawRay(transform.position, fwd * RayDistance, Color.red, 100.0f);

            }
        }
    }

    void FixedUpdate()
    {
        fwd = transform.TransformDirection(Vector3.forward);   
    }


}
