using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusicTrigger : MonoBehaviour
{
    PlayerScript playerScript;
    bool hasTriggered = false;

    AudioSource source;
    float defaultVolume;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        defaultVolume = source.volume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered)
        {
            return;
        }

        if (!playerScript.mysterySolved)
        {
            return;
        }

        hasTriggered = true;
        source.volume = defaultVolume;
    }
}
