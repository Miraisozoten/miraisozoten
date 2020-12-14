using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTutorial : Tutorial
{
    public GameObject img;
    public GameObject img2;
    public GameObject obj = null;
    private ObjectInCameraComponent InCamera;

    public KeyCode CameraResetKey;
    public Vector3 ObjectPosition;
    
    // Start is called before the first frame update
    override protected void Start()
    {
        img = CreateUIObject(img);
        obj = CreateObject(obj, Player.transform.position + ObjectPosition);
        InCamera = obj.GetComponent<ObjectInCameraComponent>();
    }

    // Update is called once per frame
    override protected void Update()
    {
        CameraDirectionTutorial();
        CameraResetTutorial();
    }

    void CameraDirectionTutorial()
    {
        if (img != null && obj != null)
        {
            if (InCamera.GetInCameraFlag())
            {
                Destroy(obj.gameObject);
                Destroy(img.gameObject);
                img2 = CreateUIObject(img2);
            }
        }
    }

    void CameraResetTutorial()
    {
        if (img == null && img2 != null)
        {
            if (Input.GetKeyDown(CameraResetKey))
            {
                Destroy(img2.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
