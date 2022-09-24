using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCdialogue : MonoBehaviour
{
    [SerializeField] float talkDistance;
    [SerializeField] GameObject talkMessage;
    [SerializeField] GameObject dialogueBubble;
    [SerializeField] TextMeshProUGUI dialogueText;
    PlayerControls controls;
    PlayerMovement playerScript;
    bool isTalking = false;
    string dialogueWords = "This is my dialogue";

    private void Awake()
    {
        controls = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        dialogueBubble.SetActive(false);
        controls.Gameplay.Interact.performed += ctx => StartConversation();
    }

    void StartConversation()
    {
        if(Vector2.Distance(playerScript.transform.position, transform.position) > talkDistance)
        {
            return;
        }

        isTalking = true;
        dialogueBubble.SetActive(true);
        dialogueText.text = dialogueWords;
        Debug.Log("Talk");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(playerScript.transform.position, transform.position) > talkDistance || isTalking)
        {
            talkMessage.SetActive(false);
        }
        else
        {
            talkMessage.SetActive(true);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
