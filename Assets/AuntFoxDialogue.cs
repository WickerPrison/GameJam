using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AuntFoxDialogue : NPCdialogue
{
    List<string> currentConversation;
    List<int> currentWhoIsTalking;

    [SerializeField] List<string> conversation1;
    [SerializeField] List<int> whoIsTalking1;
    [SerializeField] List<string> conversation1repeat;
    [SerializeField] List<int> whoIsTalking1Repeat;

    [SerializeField] List<string> conversation2;
    [SerializeField] List<int> whoIsTalking2;
    [SerializeField] List<string> conversation2repeat;
    [SerializeField] List<int> whoIsTalking2Repeat;

    int lineIndex;
    int conversationCounter = 0;
    string line1 = "Thank goodness you're here Detective Fox! My son Felix has gone missing. Can you help me find him?";
    string line2 = "No problem ma'am. I'll find your son in no time.";
    string line3 = "Wow you're so helpful, I like that in a fox! You should go look in his room, right down the hall. Maybe you'll find some clues there.";
    string repeatedLine = "You know my son is a detective too. I sure hope he's okay.";
    bool hadFirstConversation = false;
    bool hadSecondConversation = false;

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
        else
        {
            if (!hadSecondConversation)
            {
                hadSecondConversation = true;
                currentConversation = conversation2;
                currentWhoIsTalking = whoIsTalking2;
            }
            else
            {
                currentConversation = conversation2repeat;
                currentWhoIsTalking = whoIsTalking2Repeat;
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
            AuntLine(currentConversation[lineIndex]);
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

    void AuntLine(string line)
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
