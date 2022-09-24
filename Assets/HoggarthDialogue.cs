using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HoggarthDialogue : NPCdialogue
{
    [SerializeField] List<string> conversation1;
    [SerializeField] List<string> conversation1repeat;
    [SerializeField] List<int> whoIsTalking1;
    [SerializeField] List<int> whoIsTalking1Repeat;
    List<string> currentConversation;
    List<int> currentWhoIsTalking;
    int lineIndex;
    bool hadFirstConversation = false;


    public override void Start()
    {
        base.Start();
        im.controls.Dialogue.Continue.performed += ctx => NextDialogue();
    }

    public override void FirstLine()
    {
        if (!playerScript.mysterySolved)
        {
            if (!hadFirstConversation)
            {
                hadFirstConversation = true;
                currentConversation = conversation1;
                currentWhoIsTalking = whoIsTalking1;
            }
            else
            {
                currentConversation = conversation1repeat;
                currentWhoIsTalking = whoIsTalking1Repeat;
            }

            lineIndex = 0;
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        if (Vector2.Distance(playerScript.transform.position, transform.position) > talkDistance)
        {
            return;
        }

        if (lineIndex == currentConversation.Count)
        {
            EndConversation();
            return;
        }

        if (currentWhoIsTalking[lineIndex] == 0)
        {
            HoggarthLine(currentConversation[lineIndex]);
        }
        else
        {
            FoxLine(currentConversation[lineIndex]);
        }

        lineIndex += 1;
    }

    void EndConversation()
    {
        playerScript.speechBubble.SetActive(false);
        speechBubble.SetActive(false);
        im.Gameplay();
        isTalking = false;
    }

    void HoggarthLine(string line)
    {
        playerScript.speechBubble.SetActive(false);
        speechBubble.SetActive(true);
        dialogueText.text = line;
    }

    void FoxLine(string line)
    {
        speechBubble.SetActive(false);
        playerScript.SayLine(line);
    }
}
