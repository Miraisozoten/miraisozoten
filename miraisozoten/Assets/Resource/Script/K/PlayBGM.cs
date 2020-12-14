using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class PlayBGM : MonoBehaviour
{
    public bool playing = false;

    public string SoundName = "BGM/";
    // Start is called before the first frame update
    void Start(){ Play();}

    // Update is called once per frame
    void Update(){}

    protected void Play()
    {
        switch (playing)
        {
            case (true):
                break;
            case (false):
                BGMManager.Instance.Play(SoundName);
                playing = true;
                break;
        }
    }

    private void OnDestroy()
    {
        Stop();
    }

    //一時停止の再開
    void UnPause()  { BGMManager.Instance.UnPause(SoundName);   }
    //一時停止
    void Pause()    { BGMManager.Instance.Pause(SoundName);     }
    //フェードアウト
    void FadeOut()  { BGMManager.Instance.FadeOut(SoundName);   }
    //フェードイン
    void FadeIn()   { BGMManager.Instance.FadeIn(SoundName);    }
    //停止
    void Stop()     { BGMManager.Instance.Stop(SoundName);      }

}
