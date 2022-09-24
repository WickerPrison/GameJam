using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    public List<string> sawPlayerConversation = new List<string>();
    public List<string> mapConversation = new List<string>();
    List<string> currentConversation = new List<string>();
    InputManager im;
    int lineIndex;
    PlayerScript playerScript;
    bool inConversation = false;

    private void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        im = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        im.controls.Dialogue.Continue.performed += ctx => NextLine();
    }

    void NextLine()
    {
        if (!inConversation)
        {
            return;
        }

        if(lineIndex == currentConversation.Count)
        {
            EndConversation();
            return;
        }

        playerScript.SayLine(currentConversation[lineIndex]);
        lineIndex += 1;
    }

    void EndConversation()
    {
        lineIndex = 0;
        inConversation = false;
        im.Gameplay();
        playerScript.speechBubble.SetActive(false);
        playerScript.animator.Play("Idle");
    }

    public void SawPlayerConversation()
    {
        currentConversation = sawPlayerConversation;
        lineIndex = 0;
        inConversation = true;
        NextLine();
        im.Dialogue();
    }

    public void MapConversation()
    {
        currentConversation = mapConversation;
        lineIndex = 0;
        inConversation = true;
        NextLine();
        im.Dialogue();
    }
}
