using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class Samp : MonoBehaviour
{
    public bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing == false)
            SoundPlay();
    }

    void SoundPlay()
    {
        switch (playing)
        {
            case (true):
                break;
            case (false):
                BGMManager.Instance.Play("BGM/BGM_Title.mp3");
                playing = true;
                break;
        }
    }
}
