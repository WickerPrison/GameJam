using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    PlayerDialogue playerDialogue;

    private void Start()
    {
        playerDialogue = GetComponentInParent<PlayerDialogue>();
    }

    public void SawPlayer()
    {
        playerDialogue.StartConversation(playerDialogue.sawPlayerConversation);
    }
}
