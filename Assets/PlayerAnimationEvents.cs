using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    PlayerDialogue playerDialogue;
    [SerializeField] AudioClip footstep;
    AudioSource source;
    AudioSource globalSource;
    float defaultVolume;

    private void Start()
    {
        playerDialogue = GetComponentInParent<PlayerDialogue>();
        source = GetComponentInParent<AudioSource>();
        globalSource = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        defaultVolume = globalSource.volume;
    }

    public void SawPlayer()
    {
        playerDialogue.StartConversation(playerDialogue.sawPlayerConversation);
    }

    public void Win()
    {
        playerDialogue.StartConversation(playerDialogue.winConversation);
    }

    public void Footstep()
    {
        source.PlayOneShot(footstep);
    }

    public void SilenceMusic()
    {
        globalSource.volume = 0;
    }
}
