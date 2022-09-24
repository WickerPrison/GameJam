using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    InputManager im;
    [SerializeField] GameObject openMessage;
    [SerializeField] GameObject openImage;
    [SerializeField] GameObject closedImage;
    bool firstSight = true;
    PlayerScript playerScript;
    int line = 0;
    float interactDistance = 5;
    float sightDistance = 14;
    bool open = false;
    float playerDistance;
    string firstSightLine = "Hey a notebook. I happen to have one just like it. Maybe there will be something useful in there.";
    string line1 = "I'll open it to the last page and read it out loud.";
    string line2 = "\"The three Gods created the world in 24 hours. None of us existed until mere moments ago. The world is a lie\"";
    string line3 = "What an odd thing to write. I bet it is slam poetry. That stuff is always so silly.";
    string line4 = "\"You may not believe me, but it is the truth. Our actions are not our own. To see the truth all you must do is look to the side.\"";
    string line5 = "I guess I have spent my whole life looking straight ahead or straight back. I wonder what is to my side.";

    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        im.controls.Dialogue.Continue.performed += ctx => NextDialogue();
        im.controls.Gameplay.Interact.performed += ctx => OpenNotebook();
        openImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(playerScript.transform.position, transform.position);

        if (firstSight && playerDistance < sightDistance)
        {
            firstSight = false;
            im.Dialogue();
            playerScript.SayLine(firstSightLine);
        }

        if(!open && playerDistance < interactDistance)
        {
            openMessage.SetActive(true);
        }
        else
        {
            openMessage.SetActive(false);
        }
    }

    void EndDialogue()
    {
        playerScript.speechBubble.SetActive(false);
        im.Gameplay();
    }

    void NextDialogue()
    {
        if(playerDistance > sightDistance)
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
                playerScript.SayLine(line1);
                break;
            case 2:
                line += 1;
                playerScript.SayLine(line2);
                break;
            case 3:
                line += 1;
                playerScript.SayLine(line3);
                break;
            case 4:
                line += 1;
                playerScript.SayLine(line4);
                break;
            case 5:
                line += 1;
                playerScript.SayLine(line5);
                break;
            case 6:
                line += 1;
                playerScript.speechBubble.SetActive(false);
                playerScript.animator.Play("ReadNotebook");
                break;
           
        }
    }

    void OpenNotebook()
    {
        if(open || playerDistance > interactDistance)
        {
            return;
        }

        open = true;
        closedImage.SetActive(false);
        openImage.SetActive(true);
        im.Dialogue();
        line = 1;
        playerScript.SayLine(line1);
    }
}

