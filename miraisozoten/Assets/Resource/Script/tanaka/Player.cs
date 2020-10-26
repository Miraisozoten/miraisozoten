using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤー
public class Player : MonoBehaviour
{

    [SerializeField]
    private Vector3 velocity;              // 移動方向
    [SerializeField]
    private float moveSpeed = 5.0f;        // 移動速度
    [SerializeField]
    private float applySpeed = 0.2f;       // 振り向きの適用速度
    [SerializeField]
    private PlayerFollowCamera refCamera;  // カメラの水平回転を参照する用

    public GameObject cameraObject; // メインカメラへの参照

    // キャラクターコントローラ（カプセルコライダ）の参照
    private CapsuleCollider col;
    private Rigidbody rb;

    public Animator anim;                          // キャラにアタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBaseState;         // base layerで使われる、アニメーターの現在の状態の参照

    // アニメーター各ステートへの参照
    [SerializeField, Header("idle hash")]
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int runState = Animator.StringToHash("Base Layer.Run");
    static int runstopState = Animator.StringToHash("Base Layer.Run to stop");
    static int rollState = Animator.StringToHash("Base Layer.Roll");
    static int attackState = Animator.StringToHash("Base Layer.Attack");
    static int attacksoftState = Animator.StringToHash("Attack All.Attack");
    static int attackhardState = Animator.StringToHash("Attack All.Attack");
    static int attackspState = Animator.StringToHash("Attack All.Attack sp");

    // CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
    private float orgColHight;
    private Vector3 orgVectColCenter;

    public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する

    // このスイッチが入っていないとカーブは使われない
    public float useCurvesHeight = 0.5f;        // カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

    float speed;

    [SerializeField, Header("Roll時の高さ")]
    public float Rollweight;
    [SerializeField, Header("Roll時の移動")]
    public float RollSpeed;

    void Start()
    {
        cameraObject = GameObject.FindWithTag("MainCamera");

        // CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        // CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
        orgColHight = col.height;
        orgVectColCenter = col.center;

        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();

        idleState = Animator.StringToHash("Base Layer.Idle");
        attackspState = Animator.StringToHash("Attack All.Attack SP");
    }

void FixedUpdate()
    {
        Debug.Log(currentBaseState.nameHash);

        if (currentBaseState.nameHash == attackspState)
        {
            Debug.Log("aaaaaaaaaaa");
        }
        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        velocity = Vector3.zero;
        float v = 0;

        speed = moveSpeed;

        if (Input.GetKey(KeyCode.W))
        {
            velocity.z += 1;
            v = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 1;
            v = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z -= 1;
            v = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 1;
            v = 1;
        }
        if (IsAttack())
        {
            speed = 0.0f;
        }else if (IsRoll())
        {
            speed = RollSpeed;
        }

        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        velocity = velocity.normalized * speed * Time.deltaTime;
        anim.SetFloat("Speed", v);                          // Animator側で設定している"Speed"パラメタにvを渡す


        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// 参照用のステート変数にBase Layer (0)の現在のステートを設定する



        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの回転(transform.rotation)の更新
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、
            // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づけます
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.hRotation * velocity),
                                                  applySpeed);

            // プレイヤーの位置(transform.position)の更新
            // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
            transform.position += refCamera.hRotation * velocity;
        }

        //if (velocity.magnitude > 0)
        //{
        //    //transform.rotation = Quaternion.Slerp(transform.rotation,
        //    //                                      Quaternion.LookRotation(cameraObject.GetComponent<ThirdPersonCamera>().hRotation * (-velocity)),
        //    //                                      rotateSpeed);
        //    //pos = cameraObject.GetComponent<ThirdPersonCamera>().hRotation * velocity;
        //    transform.rotation = Quaternion.Slerp(transform.rotation,
        //                                          Quaternion.LookRotation(cameraObject.GetComponent<ThirdPersonCamera>().hRotation * velocity),
        //                                          rotateSpeed);
        //    transform.localPosition += cameraObject.GetComponent<ThirdPersonCamera>().hRotation * velocity;
        //    //Debug.LogError("wait");

        //    //transform.position += cameraObject.GetComponent<ThirdPersonCamera>().hRotation * velocity * Time.fixedDeltaTime;
        //}

        // 現在のベースレイヤーがrollStateの時
        else if (currentBaseState.nameHash == rollState)
        {
            // ステートがトランジション中でない場合
            if (!anim.IsInTransition(0))
            {
                // レイキャストをキャラクターのセンターから落とす
                Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                RaycastHit hitInfo = new RaycastHit();
                float RollHeight = anim.GetFloat("RollHeight");
                Debug.Log(ray);
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);

                // 高さが useCurvesHeight 以上ある時のみ、コライダーの高さと中心をJUMP00アニメーションについているカーブで調整する
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.distance > useCurvesHeight)
                    {
                        //Debug.Log(hitInfo);
                        col.height = orgColHight - RollHeight;          // 調整されたコライダーの高さ
                        float adjCenterY = orgVectColCenter.y;
                        col.center = new Vector3(0, adjCenterY, 0); // 調整されたコライダーのセンター
                    }
                    else
                    {
                        // 閾値よりも低い時には初期値に戻す（念のため）					
                        resetCollider();
                    }
                }
            }
        }
        // 現在のベースレイヤーがidleStateの時
        else if (currentBaseState.nameHash == idleState || currentBaseState.nameHash == runState) 
        {
            //カーブでコライダ調整をしている時は、念のためにリセットする
            if (useCurves)
            {
                resetCollider();
            }
            //// スペースキーを入力したらRest状態になる
            //if (Input.GetButtonDown("Jump"))
            //{
            //    //anim.SetBool ("Rest", true);
            //}
        }

    }

    public bool IsRoll()
    {
        if (currentBaseState.nameHash == rollState)
        {
            return true;
        }
        return false;
    }

    public bool IsAttack()
    {
        if (currentBaseState.nameHash == attackState)
        {
            return true;
        }
        if (currentBaseState.nameHash == attackspState)
        {
            Debug.Log("SP");
            return true;
        }
        if (currentBaseState.nameHash == attacksoftState)
        {
            Debug.Log("弱攻撃");
            return true;
        }
        if (currentBaseState.nameHash == attackhardState)
        {
            Debug.Log("強攻撃");
            return true;
        }
        return false;
    }

    void resetCollider()
    {
        // コンポーネントのHeight、Centerの初期値を戻す
        col.height = orgColHight;
        col.center = orgVectColCenter;
    }
}