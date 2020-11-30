using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    [SerializeField, Header("エフェクトobjlist")]
    public List<GameObject> EffectObjList;

    [SerializeField, Header("エフェクトlist")]
    List<ParticleSystem> EffectParticleList;

    [SerializeField, Header("敵エフェクトtrigger")]
    public bool EffectTrigger;
    [SerializeField, Header("敵が消えるまでの秒数")]
    public float DeleteTime;

    [SerializeField, Header("敵が消えるまでの秒数カウント")]
    private float GetTime;
    [SerializeField, Header("エフェクトON")]
    private bool effect;
    //[SerializeField, Header("エフェクトマテリアル")]

    //void Awake()
    //{
    //    var parent = transform;
    //    if (EffectParticleList.Count == 0)
    //    {
    //        for (int i = 0; i < EffectObjList.Count; i++)
    //        {
    //            var obj = Instantiate(EffectObjList[i]).gameObject.GetComponent<ParticleSystem>();
    //            obj.transform.parent = transform;
    //            obj.transform.position = Vector3.zero;
    //            obj.transform.rotation = Quaternion.identity;
    //            EffectParticleList.Add(obj);
    //            // EffectParticleList[i] = EffectObjList[i].GetComponent<ParticleSystem>();
    //            EffectParticleList[i].Stop();
    //        }
    //    }
        //else
        //{

        //    for (int i = 0; i < EffectObjList.Count; i++)
        //    {
        //        var obj = Instantiate(EffectObjList[i], Vector3.zero, Quaternion.identity, parent).gameObject.GetComponent<ParticleSystem>();
        //        EffectParticleList.Add(obj);
        //        // EffectParticleList[i] = EffectObjList[i].GetComponent<ParticleSystem>();
        //        EffectParticleList[i].Stop();
        //    }
        //}
    //}
    // Start is called before the first frame update
    void Start()
    {
        EffectParticleList.Clear();
        for(int i = 0; i < EffectObjList.Count; i++)
        {
            EffectParticleList.Add(EffectObjList[i].GetComponent<ParticleSystem>());
            EffectParticleList[i].Stop();
        }
        GetTime = 0.0f;
        EffectTrigger = false;
        effect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EffectTrigger)
        {
            GetTime += Time.deltaTime;

            if (GetTime > DeleteTime)
            {
                GetTime = 0.0f;
                EffectTrigger = false;
                Destroy(gameObject);
            }
        }

        if (effect)
        {
            EffectOn();
            effect=false;
        }
    }

    public void EffectOn()
    {
        EffectTrigger = true;

        for(int i = 0; i < EffectParticleList.Count; i++)
        {
            EffectParticleList[i].Play();
        }
    }
}
