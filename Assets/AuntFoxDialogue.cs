using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AuntFoxDialogue : NPCdialogue
{
    int line = 1;
    int conversationCounter = 0;
    string line1 = "Thank goodness you're here Detective Fox! My son Felix has gone missing. Can you help me find him?";
    string line2 = "No problem ma'am. I'll find your son in no time.";
    string line3 = "Wow you're so helpful, I like that in a fox! You should go look in his room, right down the hall. Maybe you'll find some clues there.";
    string repeatedLine = "You know my son is a detective too. I sure hope he's okay.";

    public override void Start()
    {
        base.Start();
        im.controls.Dialogue.Continue.performed += ctx => NextDialogue();
    }

    public override void FirstLine()
    {
        switch (conversationCounter)
        {
            case 0:
                conversationCounter += 1;
                playerScript.speechBubble.SetActive(false);
                speechBubble.SetActive(true);
                dialogueText.text = line1;
                break;
            default:
                line = 0;
                RepeatedLIne();
                break;
        }
    }

    void SecondLine()
    {
        speechBubble.SetActive(false);
        playerScript.SayLine(line2);
    }

    void ThirdLine()
    {
        playerScript.speechBubble.SetActive(false);
        speechBubble.SetActive(true);
        dialogueText.text = line3;
    }

    void RepeatedLIne()
    {
        playerScript.speechBubble.SetActive(false);
        speechBubble.SetActive(true);
        dialogueText.text = repeatedLine;
    }

    void EndDialogue()
    {
        playerScript.speechBubble.SetActive(false);
        speechBubble.SetActive(false);
        im.Gameplay();
        isTalking = false;
    }

    void NextDialogue()
    {
        if (Vector2.Distance(playerScript.transform.position, transform.position) > talkDistance)
        {
            return;
        }

        switch (line)
        {
            case 0:
                EndDialogue();
                break;
            case 1:
                line += 1;
                SecondLine();
                break;
            case 2:
                line += 1;
                ThirdLine();
                break;
            case 3:
                EndDialogue();
                break;
        }
    }
}
