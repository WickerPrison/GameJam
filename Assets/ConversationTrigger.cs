using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    [SerializeField] List<string> conversation;
    [SerializeField] bool lookAtPlayer;
    PlayerDialogue playerDialogue;
    PlayerScript playerScript;
    bool hasTriggered = false;
    [SerializeField] bool backJourney;

    private void Start()
    {
        playerDialogue = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDialogue>();
        playerScript = playerDialogue.gameObject.GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hasTriggered)
        {
            return;
        }

        if(backJourney && !playerScript.mysterySolved)
        {
            return;
        }

        hasTriggered = true;
        playerDialogue.StartConversation(conversation);
        if (lookAtPlayer)
        {
            playerScript.animator.Play("LookAtPlayer");
        }
    }
}
