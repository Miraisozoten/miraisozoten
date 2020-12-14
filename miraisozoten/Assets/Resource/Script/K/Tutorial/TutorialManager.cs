using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TUTORIAL_STATUS
{
    MOVE,                   //移動
    AVOIDANCE,              //回避ローリング
    CAMERA_MOVE,            //カメラ操作
    CAMERA_RESET,           //カメラのリセット
    LIGHT_ATTACK,           //弱攻撃
    STRONG_ATTACK,          //強攻撃
    JUICE_ACTION_SELECT,    //エキスアクションの選択
    JUICE_ACTION,           //エキスアクション
};

public enum TUTORIAL_MANAGER_STATE
{
    MOVE,
    CAMERA,
    ATTACK
}

public class TutorialManager : MonoBehaviour
{
    public Vector3 UIPosition;
    public Vector3 UIScale;

    //引き渡し用オブジェクト群
    public GameObject Player;

    //チュートリアルオブジェクト群
    public GameObject move;
    public GameObject camera_move;
    public GameObject attack;

    // Start is called before the first frame update
    void Start()
    {
        if (UIPosition == null)
            Debug.LogError("UIPositionがnullです。");
        if (Player == null)
            Debug.LogError("Playerの値がnullです。");

        this.transform.position = Player.transform.position;

        move = CreateGameObject(move);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private GameObject CreateGameObject(GameObject obj)
    {
        if (obj != null)
        {
            GameObject obj2 = (GameObject)Instantiate(obj, this.transform.position, Quaternion.identity);
            obj2.transform.parent = this.transform;
            return obj2;
        }
        Debug.LogError("objの値がnullです。");
        return null;
    }
    
    public void ChangeEventObject(TUTORIAL_MANAGER_STATE s)
    {
        Delete();
        switch (s)
        {
            case (TUTORIAL_MANAGER_STATE.MOVE):
                move = CreateGameObject(move);
                break;
            case (TUTORIAL_MANAGER_STATE.CAMERA):
                camera_move = CreateGameObject(camera_move);
                break;
            case (TUTORIAL_MANAGER_STATE.ATTACK):
                attack = CreateGameObject(attack);
                break;
        }
    }

    private void Delete()
    {
        foreach(Transform n in this.gameObject.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }
}
