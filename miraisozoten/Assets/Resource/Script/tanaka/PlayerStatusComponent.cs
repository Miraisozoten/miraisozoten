using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusComponent : StatusComponent
{
    [SerializeField, Header("HP_Icon")]
    public float HP_Icon;
    [SerializeField, Header("HP-Gauge")]
    public Slider HP_Gauge;
    [SerializeField, Header("HP-Color")]
    public Image BerImage;
    [SerializeField, Header("体力減少速度/s")]
    public float HP_Decrease;
    [SerializeField, Header("最大体力(ゲージ)")]
    public float HP_Max_Gauge;
    [SerializeField, Header("体力減少量/ハート半分")]
    public int HP_Amount;
    [SerializeField, Header("最大体力(アイコン数)")]
    public int HP_Max_Icon;
    [SerializeField, Header("体力アイコン左")]
    public GameObject LeftIcon;
    [SerializeField, Header("体力アイコン右")]
    public GameObject RightIcon;
    [SerializeField, Header("体力アイコンパネル")]
    public GameObject IconPanel;

    //[SerializeField, Header("カラーG")]
    private float Color_G;
    //[SerializeField, Header("カラーR")]
    private float Color_R;
    [SerializeField, Header("アイコンリスト")]
    public List<GameObject> LifeList;
    [SerializeField, Header("アイコン減少ボタン間隔/s")]
    public float ButtonInterval;
    private float Timebutton;

    [SerializeField, Header("武器")]
    public GameObject WeaponObj;
    private BoxCollider WeaponColider;

    private Animator anim;                          // キャラにアタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照


    // Start is called before the first frame update
    void Start()
    {
        //HP = 100;
        HP = HP_Max_Gauge;
        HP_Icon = HP_Max_Icon;

        for (int i = 0; i < HP_Max_Icon; i++)
        {
            LifeList.Add(Instantiate<GameObject>(LeftIcon, IconPanel.transform));
            LifeList.Add(Instantiate<GameObject>(RightIcon, IconPanel.transform));
        }

        anim = GetComponent<Animator>();

        WeaponColider = WeaponObj.GetComponent<BoxCollider>();
        WeaponColider.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim.GetBool("Attack"))
        {
            anim.SetBool("Attack", false);
        }
        if (anim.GetBool("Roll"))
        {
            anim.SetBool("Roll", false);
        }

        Timebutton += Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.F1))
        {
            HP += HP_Decrease * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.F2))
        {
            HP -= HP_Decrease * Time.fixedDeltaTime;
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("Attack", true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetBool("Roll", true);
        }

        if (HP_Max_Icon >= HP_Icon)
        {
            IconAll();
        }
        else
        {
            HP_Icon = HP_Max_Icon;
        }
        if (Input.GetKeyDown(KeyCode.F5) || LifeList.Count != HP_Max_Icon * 2) 
        {
            IconReset();
        }

        GaugeAll();
    }

    void GaugeAll()
    {
        HP_Gauge.value = HP / HP_Max_Gauge;
        Color_G = 2.0f - (1.0f - HP_Gauge.value) * 2;
        Color_R = 2.0f - HP_Gauge.value * 2;
        BerImage.color = new Vector4(Color_R, Color_G, 0, BerImage.color.a);
        if (HP > HP_Max_Gauge)
        {
            HP = HP_Max_Gauge;
        }
        if (HP < 0)
        {
            HP = 0;
        }
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
            //if (IconPanel.transform.childCount > HP_Icon)
            //{
            //    for (int i = IconPanel.transform.childCount; i > HP_Icon; i--)
            //    {
            //        //　ライフゲージoff
            //        LifeList[i].SetActive(false);
            //        //Destroy(IconPanel.transform.GetChild(i).gameObject);
            //        //Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject);
            //    }
            //}else if(IconPanel.transform.childCount < HP_Icon)
            //{
            //    for (int i = IconPanel.transform.childCount; i > HP_Icon; i--)
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
    }

    void IconReset()
    {
        //HP_Max_Iconが変動時一度リセット
        LifeList.Clear();

        //HP設定
        HP_Icon = HP_Max_Icon;
        //削除
        for (int i = 0; i < IconPanel.transform.childCount; i++)
        {
            Destroy(IconPanel.transform.GetChild(i).gameObject);
        }
        //生成
        for (int i = 0; i < HP_Max_Icon; i++)
        {
            LifeList.Add(Instantiate<GameObject>(LeftIcon, IconPanel.transform));
            LifeList.Add(Instantiate<GameObject>(RightIcon, IconPanel.transform));
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
}

