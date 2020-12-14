using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private TutorialManager tm;

    public Vector3 UIPosition;
    public Vector3 UIScale;

    public TUTORIAL_MANAGER_STATE NextTutorial;

    protected GameObject Player;

    protected GameObject canvas;

    //1.TutorialManager 2.Player 3.canvas
    virtual public bool Init(params GameObject[] obj)
    {
        return true;
    }

    protected void Awake()
    {
        tm = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        if (tm == null)
            Debug.LogError("tmの値がnullです。");
        Player = tm.Player;
        if (Player == null)
            Debug.LogError("Playerの値がnullです。");
        canvas = GameObject.Find("TutorialCanvas");
        if (canvas == null)
            Debug.LogError("canvasの値がnullです。");
        UIPosition = tm.UIPosition;
        UIScale = tm.UIScale;
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {

    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }

    protected void OnDestroy()
    {
        Next();

        DestroyChildObjects(this.gameObject);
    }
    //次のチュートリアルへ移動
    protected void Next()
    {
        tm.ChangeEventObject(NextTutorial);
    }
    //オブジェクトの生成
    protected GameObject CreateObject(GameObject obj)
    {
        obj = (GameObject)Instantiate(obj, new Vector3(UIPosition.x,UIPosition.y,UIPosition.z), Quaternion.identity);
        return obj;
    }
    //ChildをParentの子オブジェクトにする。
    protected GameObject SetParent(GameObject Child){
        Child.transform.SetParent(canvas.transform, false);
        return Child;
    }
    //protected void SetPosition(GameObject obj)
    //{
    //    obj.GetComponent<UIPositionComponent>().Init(UIPosition);
    //}
    protected GameObject CreateUIObject(GameObject obj)
    {
        obj = CreateObject(obj);
        obj.transform.localScale = UIScale;
        obj = SetParent(obj);
        return obj;
    }
    //オブジェクトの破棄
    protected GameObject DestroyObject(GameObject obj)
    {
        Destroy(obj.gameObject);
        return null;
    }
    protected void DestroyChildObjects(GameObject obj)
    {
        foreach (Transform n in obj.gameObject.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }
    protected GameObject CreateObject(GameObject obj,Vector3 pos)
    {
        obj = (GameObject)Instantiate(obj, pos, Quaternion.identity);
        return obj;
    }
}