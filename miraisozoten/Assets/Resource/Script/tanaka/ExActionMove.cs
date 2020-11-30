using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExActionMove : MonoBehaviour
{
    public List<Canvas> ActionList;
    int actionsort;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            ActionList.Add(gameObject.transform.GetChild(i).GetComponent<Canvas>());
        }

        Debug.Log(ActionList[0].sortingOrder);
        ActionList[0].sortingOrder = 1;
        ActionList[1].sortingOrder = 0;
        ActionList[2].sortingOrder = 1;
        actionsort = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void SetUIDown()
    {
        for(int i = 0; i < ActionList.Count; i++)
        {
            ActionList[i].sortingOrder += 1;
            if (ActionList[i].sortingOrder > 2)
            {
                ActionList[i].sortingOrder = 0;
            }
        }
    }
    public void SetUIUp()
    {

    }
}
