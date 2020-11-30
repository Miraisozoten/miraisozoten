using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// プレイヤー
public class Player : MonoBehaviour
{
    enum ExActionName
    {
        Heal = 0,
        Spattack,

    };

    //[SerializeField]
    private Vector3 velocity;              // 移動方向
    [SerializeField, Header("プレイヤーの移動速度")]
    private float moveSpeed = 5.0f;        // 移動速度
    //[SerializeField]
    private float applySpeed = 0.2f;       // 振り向きの適用速度
    //[SerializeField]
    private PlayerFollowCamera refCamera;  // カメラの水平回転を参照する用
    [SerializeField, Header("メインカメラを入れる")]
    public GameObject cameraObject; // メインカメラへの参照
    [SerializeField, Header("敵レイヤーの名前")]
    public string EnemyLayerName;
    [SerializeField, Header("敵との接触判定の可否")]
    public bool Hittriger;
    [SerializeField, Header("現在のエキスアクション")]
    public int ExAction;
    [SerializeField, Header("エキスボタン入れる")]
    public List<GameObject> ExButtonObj;

    [SerializeField, Header("ホイール間隔")]
    public float WheelTime;
    float TimeCount;
    [SerializeField, Header("ホイールトリガー")]
    public bool WheelTrigger;


    [SerializeField, Header("以下は触らないで")]

    // キャラクターコントローラ（カプセルコライダ）の参照
    private CapsuleCollider col;
    private Rigidbody rb;

    private Animator anim;                          // キャラにアタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBaseState;         // base layerで使われる、アニメーターの現在の状態の参照

    // アニメーター各ステートへの参照
    //[SerializeField, Header("idle hash")]
    static int idleState = Animator.StringToHash("Nomal Layer.Idle");
    static int runState = Animator.StringToHash("Nomal Layer.Run");
    static int runstopState = Animator.StringToHash("Nomal Layer.Run to stop");
    static int rollState = Animator.StringToHash("Attack Layer.Roll");
    static int attackState = Animator.StringToHash("Attack Layer.Attack");
    static int attacksoftState = Animator.StringToHash("Attack Layer.Attack soft");
    static int attackhardState = Animator.StringToHash("Attack Layer.Attack hard");
    static int attackspState = Animator.StringToHash("Attack Layer.Attack SP");
    static int HitState = Animator.StringToHash("Hit Layer.Hit");
    static int Hit1State = Animator.StringToHash("Hit Layer.Hit1");
    static int Hit2State = Animator.StringToHash("Hit Layer.Hit2");
    static int Hit3State = Animator.StringToHash("Hit Layer.Hit3");
    static int StandUpState = Animator.StringToHash("Hit Layer.StandUp");

    // CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
    private float orgColHight;
    private Vector3 orgVectColCenter;

    public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する

    // このスイッチが入っていないとカーブは使われない
    public float useCurvesHeight = 0.5f;        // カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

    float speed;
    float v = 0;

    [SerializeField, Header("Roll時の高さ")]
    public float Rollweight;
    [SerializeField, Header("Roll時の移動")]
    public float RollSpeed;

    PlayerStatusComponent p_Status;

    void Start()
    {
        //cameraObject = GameObject.FindWithTag("MainCamera");
        refCamera = cameraObject.GetComponent<PlayerFollowCamera>();
        // CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        // CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
        orgColHight = col.height;
        orgVectColCenter = col.center;

        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
        p_Status = GetComponent<PlayerStatusComponent>();
        //idleState = Animator.StringToHash("Base Layer.Idle");
        //attackspState = Animator.StringToHash("Attack All.Attack SP");

        WheelTrigger = false;

        idleState = Animator.StringToHash("Nomal Layer.Idle");
        runState = Animator.StringToHash("Nomal Layer.Run");
        runstopState = Animator.StringToHash("Nomal Layer.Run to stop");
        rollState = Animator.StringToHash("Attack Layer.Roll");
        attackState = Animator.StringToHash("Attack Layer.Attack");
        attacksoftState = Animator.StringToHash("Attack Layer.Attack soft");
        attackhardState = Animator.StringToHash("Attack Layer.Attack hard");
        attackspState = Animator.StringToHash("Attack Layer.Attack SP");
        HitState = Animator.StringToHash("Hit Layer.Hit");
        Hit1State = Animator.StringToHash("Hit Layer.Hit1");
        Hit2State = Animator.StringToHash("Hit Layer.Hit2");
        Hit3State = Animator.StringToHash("Hit Layer.Hit3");
        StandUpState = Animator.StringToHash("Hit Layer.StandUp");
    }

void FixedUpdate()
    {
        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        velocity = Vector3.zero;

        speed = moveSpeed;
        v = 0;

        //マウスホイール取得
        if (WheelTrigger)
        {
            TimeCount += Time.deltaTime;
        }
        else
        {
            float Wheel = Input.GetAxis("Mouse ScrollWheel");
            SetWheelMove(Wheel);
        }

        if (WheelTime < TimeCount)
        {
            WheelTrigger = false;
            TimeCount = 0.0f;
        }

        KeyAction();

        if (IsAttack()||IsHit())
        {
            speed = 0.0f;
        }else if (IsRoll())
        {
            speed = RollSpeed;
        }

        SetAnimation(v);
        
    }

    void KeyAction()
    {
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
        if (!anim.GetBool("Attack hard") && !anim.GetBool("Attack soft") && !anim.GetBool("Attack sp"))
        {
            if (Input.GetMouseButtonDown(1) && p_Status.ExpNow() > 0)
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Attack hard", true);
                p_Status.ExpDown();

            }
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Attack soft", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //選択されているエキスアクションを実行
            SetExAction(ExAction);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetBool("Roll", true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetBool("HitChack", true);
        }
    }
    void SetAnimation(float v)
    {

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
         if (currentBaseState.nameHash == rollState)
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
            if (!anim.GetBool("Attack"))
            {
                anim.SetBool("Attack soft", false);
                anim.SetBool("Attack hard", false);
                anim.SetBool("Attack sp", false);
            }
            //// スペースキーを入力したらRest状態になる
            //if (Input.GetButtonDown("Jump"))
            //{
            //    //anim.SetBool ("Rest", true);
            //}
        }
        else if (currentBaseState.nameHash == Hit1State || currentBaseState.nameHash == Hit2State || currentBaseState.nameHash == Hit3State)
        {
            if (anim.GetBool("HitChack"))
            {
                anim.SetBool("HitChack", false);
            }
        } else if (currentBaseState.nameHash == attackState || currentBaseState.nameHash == attacksoftState || currentBaseState.nameHash == attackhardState || currentBaseState.nameHash == attackspState) 
        {
            anim.SetBool("Attack", false);
        }

        if (anim.IsInTransition(0))
        {
            if (anim.GetBool("Attack"))
            {
            }
            if (anim.GetBool("Roll"))
            {
                anim.SetBool("Roll", false);
            }
        }
    }

    //エキスアクション(アクション名)
    void SetExAction(int actionNumber)
    {
        int exAction = actionNumber % 3;

        if (exAction < 0)
        {
            exAction *= -1;
        }

        //エキスアクションの選択
        switch (exAction)
        {
            //回復
            case (int)ExActionName.Heal:
                Debug.Log("Heal");
                break;

            //必殺技
            case (int)ExActionName.Spattack:
                if (!anim.GetBool("Attack hard") && !anim.GetBool("Attack soft") && !anim.GetBool("Attack sp"))
                    if (p_Status.MaxExp() == p_Status.ExpNow())
                    {
                        anim.SetBool("Attack", true);
                        anim.SetBool("Attack sp", true);
                        p_Status.ExpZero();

                    }
                break;

            default:
                break;
        }
    }
    //マウスホイール操作
    int SetWheelMove(float mouseWheel)
    {
        ExAction += (int)(mouseWheel);

        if (mouseWheel > 0)
        {
            ExAction++;
            WheelTrigger = true;
        }
        else if (mouseWheel < 0)
        {
            ExAction--;
            WheelTrigger = true;
        }
        else if (mouseWheel == 0)
        {

        }

        ExButtonObj[0].GetComponent<Image>().color = Color.white;
        ExButtonObj[1].GetComponent<Image>().color = Color.white;
        ExButtonObj[2].GetComponent<Image>().color = Color.white;

        int aaa = ExAction % 3;
        if (aaa < 0)
        {
            aaa *= -1;
        }
        ExButtonObj[aaa].GetComponent<Image>().color = Color.red;

        //switch (aaa)
        //{
        //    case (int)ExActionName.Heal:
        //        ExButtonObj[aaa].GetComponent<Image>().color = Color.red;
        //        break;
        //    case (int)ExActionName.Spattack:
        //        ExButtonObj[aaa].GetComponent<Image>().color = Color.red;
        //        break;
        //    case 2:
        //        ExButtonObj[aaa].GetComponent<Image>().color = Color.red;
        //        break;
        //}
        return 0;
    }
    public bool IsHit()
    {
        if (currentBaseState.nameHash == Hit1State || currentBaseState.nameHash == Hit2State || currentBaseState.nameHash == Hit3State)
        {
            return true;
        }
        return false;
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

    //のけぞり処理
    void HitEnemyAttack()
    {
        anim.SetBool("HitChack", true);
        anim.SetInteger("HitNumber", 3);
    }

    //敵との当たり判定
    //void OnCollisionEnter(Collider col)
    void OnTriggerEnter(Collider col)
    {
        if (LayerMask.LayerToName(col.gameObject.layer) == EnemyLayerName&&Hittriger)
        {
            Debug.Log("敵と接触");
            v = 0.0f;
            HitEnemyAttack();
        }
    }

    //現在選択されているエキスアクション取得
    public int GetExAction()
    {
        return ExAction;
    }
}