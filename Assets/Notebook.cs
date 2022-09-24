using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    InputManager im;
    bool firstSight = true;
    PlayerScript playerScript;
    int line = 0;
    string firstSightLine = "Hey a notebook. I happen to have one just like it. Maybe there will be something useful in there.";

    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        im.controls.Dialogue.Continue.performed += ctx => NextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(firstSight && Vector3.Distance(playerScript.transform.position, transform.position) < 14)
        {
            firstSight = false;
            im.Dialogue();
            playerScript.SayLine(firstSightLine);
        }
    }

    void EndDialogue()
    {
        playerScript.speechBubble.SetActive(false);
        im.Gameplay();
    }

    void NextDialogue()
    {
        switch (line)
        {
            case 0:
                EndDialogue();
                break;
        }
    }
}

