using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]

public class PlayerStatusComponent : StatusComponent
{
    //Gauge
    //[SerializeField, Header("HP-Gauge")]
    //public Slider HP_Gauge;
    //[SerializeField, Header("HP-Color")]
    //public Image BerImage;
    //[SerializeField, Header("体力減少速度/s")]
    //public float HP_Decrease;
    //[SerializeField, Header("最大体力(ゲージ)")]
    //public float HP_Max_Gauge;
    /////////////////////////////////////////////////////////////

    //Icon
    [SerializeField, Header("HP量")]
    public float HP_Icon;
    [SerializeField, Header("エキス量")]
    public int ExtractPoint_Icon;
    [SerializeField, Header("体力減少量/ハート半分")]
    public int HP_Amount;
    [SerializeField, Header("最大体力(アイコン数)")]
    public int HP_Max_Icon;
    [SerializeField, Header("体力アイコン")]
    public GameObject Icon;
    //[SerializeField, Header("体力アイコン左")]
    //public GameObject LeftIcon;
    //[SerializeField, Header("体力アイコン右")]
    //public GameObject RightIcon;
    [SerializeField, Header("体力アイコンパネル")]
    public GameObject HpIconPanel;

    [SerializeField, Header("最大エキス(アイコン数)")]
    public int Ex_Max_Icon;
    [SerializeField, Header("エキスアイコン")]
    public GameObject ExPointIcon;
    [SerializeField, Header("エキスアイコンパネル")]
    public GameObject ExPanel;
    /////////////////////////////////////////////////////////////

    //[SerializeField, Header("カラーG")]
    private float Color_G;
    //[SerializeField, Header("カラーR")]
    private float Color_R;
    [SerializeField, Header("HPアイコンリスト")]
    public List<GameObject> LifeList;
    [SerializeField, Header("エキスアイコンリスト")]
    public List<GameObject> ExList;
    [SerializeField, Header("アイコン減少ボタン間隔/s")]
    public float ButtonInterval;
    private float Timebutton;

    [SerializeField, Header("武器")]
    public GameObject WeaponObj1;
    public GameObject WeaponObj2;
    private BoxCollider[] WeaponColider;

    //private Animator anim;                          // キャラにアタッチされるアニメーターへの参照
    //private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照

    private Player playerCom;
    
    // Start is called before the first frame update
    void Start()
    {
        //HP = 100;
        //HP = HP_Max_Gauge;
        HP_Icon = HP_Max_Icon;
        ExtractPoint_Icon = 0;

        for (int i = 0; i < HP_Max_Icon; i++)
        {
            LifeList.Add(Instantiate<GameObject>(Icon, HpIconPanel.transform));
            //LifeList.Add(Instantiate<GameObject>(LeftIcon, HpIconPanel.transform));
            //LifeList.Add(Instantiate<GameObject>(RightIcon, HpIconPanel.transform));
        }
        for (int i = 0; i < Ex_Max_Icon; i++)
        {
            ExList.Add(Instantiate<GameObject>(ExPointIcon, ExPanel.transform));
        }

        //anim = GetComponent<Animator>();
        WeaponColider = new BoxCollider[2];
        WeaponColider[0] = WeaponObj1.GetComponent<BoxCollider>();
        WeaponColider[0].enabled = false;
        WeaponColider[1] = WeaponObj2.GetComponent<BoxCollider>();
        WeaponColider[1].enabled = false;

        playerCom = GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Timebutton += Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.F1))
        {
            ExpUp();
            //HP += HP_Decrease * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.F2))
        {
            ExpDown();
            //HP -= HP_Decrease * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.F3) && Timebutton > ButtonInterval)
        {
            Timebutton = 0;
            HP_Icon += HP_Amount * 0.5f;
        }
        if (Input.GetKey(KeyCode.F4) && Timebutton > ButtonInterval)
        {
            Timebutton = 0;
            HP_Icon -= HP_Amount * 0.5f;
        }


        if (playerCom.IsAttack())
        {
            WeaponColider[0].enabled = true;
            WeaponColider[1].enabled = true;
        }
        else
        {
            WeaponColider[0].enabled = false;
            WeaponColider[1].enabled = false;
        }

        if (HP_Max_Icon >= HP_Icon)
        {
        }
        else
        {
            HP_Icon = HP_Max_Icon;
        }
        if (Input.GetKeyDown(KeyCode.F5) || LifeList.Count != HP_Max_Icon * 2) 
        {
            IconReset();
        }

        if (ExtractPoint_Icon > Ex_Max_Icon)
        {
            ExtractPoint_Icon = Ex_Max_Icon;
        }
        //GaugeAll();
        IconAll();

    }

    void GaugeAll()
    {
        //HP_Gauge.value = HP / HP_Max_Gauge;
        //Color_G = 2.0f - (1.0f - HP_Gauge.value) * 2;
        //Color_R = 2.0f - HP_Gauge.value * 2;
        //BerImage.color = new Vector4(Color_R, Color_G, 0, BerImage.color.a);
        //if (HP > HP_Max_Gauge)
        //{
        //    HP = HP_Max_Gauge;
        //}
        //if (HP < 0)
        //{
        //    HP = 0;
        //}
    }

    void IconAll()
    {
        //HP_Icon変動時
        if (HP_Icon >= 0)
        {
            for (int i = 0; i < LifeList.Count; i++)
            {
                if (i < HP_Icon * 2)
                {
                    if (LifeList[i].active == false)
                    {
                        LifeList[i].SetActive(true);
                    }
                }
                else
                {
                    if (LifeList[i].active == true)
                    {
                        LifeList[i].SetActive(false);
                    }
                }
            }
            //if (HpIconPanel.transform.childCount > HP_Icon)
            //{
            //    for (int i = HpIconPanel.transform.childCount; i > HP_Icon; i--)
            //    {
            //        //　ライフゲージoff
            //        LifeList[i].SetActive(false);
            //        //Destroy(HpIconPanel.transform.GetChild(i).gameObject);
            //        //Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject);
            //    }
            //}else if(HpIconPanel.transform.childCount < HP_Icon)
            //{
            //    for (int i = HpIconPanel.transform.childCount; i > HP_Icon; i--)
            //    {
            //        //　ライフゲージoff
            //        LifeList[i].SetActive(false);
            //    }
            //}
        }
        if (HP_Icon > HP_Max_Icon)
        {
            HP_Icon = HP_Max_Icon;
        }
        if (HP_Icon < 0)
        {
            HP_Icon = 0;
        }

        //ExtractPoint_Icon変動時
        if (ExtractPoint_Icon >= 0)
        {
            for (int i = 0; i < ExList.Count; i++)
            {
                if (i < ExtractPoint_Icon)
                {
                    if (ExList[i].active == false)
                    {
                        ExList[i].SetActive(true);
                    }
                }
                else
                {
                    if (ExList[i].active == true)
                    {
                        ExList[i].SetActive(false);
                    }
                }
            }
        }
        if (ExtractPoint_Icon > Ex_Max_Icon)
        {
            ExtractPoint_Icon = Ex_Max_Icon;
        }
        if (ExtractPoint_Icon < 0)
        {
            ExtractPoint_Icon = 0;
        }

    }

    void IconReset()
    {
        //HP_Max_Iconが変動時一度リセット
        LifeList.Clear();

        //HP設定
        HP_Icon = HP_Max_Icon;
        //削除
        for (int i = 0; i < HpIconPanel.transform.childCount; i++)
        {
            Destroy(HpIconPanel.transform.GetChild(i).gameObject);
        }
        //生成
        for (int i = 0; i < HP_Max_Icon; i++)
        {
            LifeList.Add(Instantiate<GameObject>(Icon, HpIconPanel.transform));
            //LifeList.Add(Instantiate<GameObject>(LeftIcon, HpIconPanel.transform));
            //LifeList.Add(Instantiate<GameObject>(RightIcon, HpIconPanel.transform));
        }
    }

    public bool HP_GaugeSet()
    {
        if (HP <= 0)
        {
            return true;
        }

        return false;
    }
    public bool HP_IconSet()
    {
        if (HP_Icon <= 0)
        {
            return true;
        }

        return false;
    }

    //HP上昇(上昇量 float)
    public void HPUp(float damage)
    {
        HP_Icon += damage;
    }
    //HP減少(減少量 float)
    public void HPDown(float damage)
    {
        HP_Icon -= damage;
    }

    //エキス上昇(1メモリ)
    public void ExpUp()
    {
        ExtractPoint_Icon += 1;
    }
    //エキス減少(1メモリ)
    public void ExpDown()
    {
        ExtractPoint_Icon -= 1;
    }
    //エキス減少(1メモリ)
    public void ExpZero()
    {
        ExtractPoint_Icon = 0;
    }
    //現在のエキス量
    public int ExpNow()
    {
        Debug.Log(ExtractPoint_Icon + "現在エキス量");
        return ExtractPoint_Icon;
    }
    //最大エキス量
    public int MaxExp()
    {
        Debug.Log(Ex_Max_Icon + "最大エキス量");
        return Ex_Max_Icon;
    }

    public bool GetWeaponCollider()
    {
        return WeaponColider[0].enabled;
    }
}

