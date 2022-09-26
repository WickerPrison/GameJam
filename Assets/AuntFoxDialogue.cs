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
    bool hadFirstConversation = false;
    bool hadSecondConversation = false;
    bool onlyWinOnce = false;

    AudioSource source;


    public override void Start()
    {
        base.Start();
        im.controls.Dialogue.Continue.performed += ctx => NextDialogue();
        source = im.gameObject.GetComponent<AudioSource>();
    }

    public override void FirstLine()
    {
        if (playerScript.hasWon)
        {
            return;
        }

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
        if (playerScript.hasWon)
        {
            return;
        }

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
        if (hadSecondConversation && !onlyWinOnce)
        {
            onlyWinOnce = true;
            source.volume = 0;
            im.YouWin();
        }
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
