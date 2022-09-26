using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicTrigger : MonoBehaviour
{
    AudioSource source;
    bool onlyTriggerOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onlyTriggerOnce)
        {
            onlyTriggerOnce = true;
            source.volume = 0;
        }
    }
}
