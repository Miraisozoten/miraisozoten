using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class PlayBossBGM : PlayBGM
{
    bool IsTrigger = false;

    public GameObject Player = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {}

    private void OnTriggerEnter(Collider other)
    {
        if (other == Player.GetComponent<Collider>())
        {
            switch (IsTrigger)
            {
                case (true):
                    break;
                case (false):
                    Play();
                    IsTrigger = true;
                    break;
            }
        }
    }
}
