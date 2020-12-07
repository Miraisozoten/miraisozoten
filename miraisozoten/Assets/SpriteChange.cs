using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI処理のクラスを使用する宣言
using UnityEngine.UI;

//==Imageのソーススプライトを動的に切り替える処理==
public class SpriteChange : MonoBehaviour
{
    // Imageコンポーネントを格納する変数
    private Image m_Image;
    //スプライト画像の透明化
    [SerializeField,Header("スプライトの透明度(0 ~ 1.0まで)")]
    public float alpha;
    // スプライトオブジェクトを格納する配列
    public Sprite[] m_Sprite;
    // trueでチェンジ,falseで停止
    bool Change;
    //切り替える画像の番号を格納する変数
    int i;

    // ゲーム開始時に実行する処理
    void Start()
    {
        // !お試しのためtrueで再生している!
        Change = true;
        // Imageコンポーネントを取得して変数 m_Image に格納
        m_Image = GetComponent<Image>();
        //初期は０番地の画像をセット
        i = 0;
    }

    // ３０フレームで挙動するようにしている(Unity側でも設定変更が必要！！ =0.3333 )
    void FixedUpdate()
    {
        m_Image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        //スプライトオブジェクト切り替え処理
        if(Change == true)
        {
          m_Image.sprite = m_Sprite[i];
          i++;
        }

        //上限に達すると停止させる処理
        if(i > 150)
        {
          Change = false;
        }

    }
}
