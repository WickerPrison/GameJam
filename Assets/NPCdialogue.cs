using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCdialogue : MonoBehaviour
{
    public float talkDistance;
    [SerializeField] GameObject talkMessage;
    public GameObject speechBubble;
    SpriteRenderer speechBubbleRenderer;
    [SerializeField] Vector3 textPositionLeft;
    [SerializeField] Vector3 textPositionRight;
    [SerializeField] RectTransform speechBubbleCanvas;
    public TextMeshProUGUI dialogueText;
    public PlayerScript playerScript;
    public bool isTalking = false;
    string dialogueWords = "This is my dialogue";
    Vector3 initialScale;
    [SerializeField] Animator animator;
    Vector3 textBubblePosition;
    Vector3 reverseX = new Vector3(-1, 1, 1);
    public InputManager im;

    // Start is called before the first frame update
    public virtual void Start()
    {
        im = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        speechBubbleRenderer = speechBubble.GetComponent<SpriteRenderer>();
        speechBubble.SetActive(false);
        im.controls.Gameplay.Interact.performed += ctx => StartConversation();
        im.controls.Dialogue.Disable();
        initialScale = animator.transform.localScale;
        textBubblePosition = speechBubble.transform.localPosition;
    }

    void StartConversation()
    {
        if(Vector2.Distance(playerScript.transform.position, transform.position) > talkDistance)
        {
            return;
        }

        if(playerScript.transform.position.x < transform.position.x)
        {
            playerScript.LookRight();
        }
        else
        {
            playerScript.LookLeft();
        }

        im.Dialogue();
        isTalking = true;

        FirstLine();
    }

    public virtual void FirstLine()
    {
        speechBubble.SetActive(true);
        dialogueText.text = dialogueWords;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.hasWon)
        {
            return;
        }

        if(Vector2.Distance(playerScript.transform.position, transform.position) > talkDistance || isTalking) 
        {
            talkMessage.SetActive(false);
        }
        else
        {
            talkMessage.SetActive(true);
        }

        if (playerScript.transform.position.x > transform.position.x)
        {
            LookRight();
        }
        else if (playerScript.transform.position.x < transform.position.x)
        {
            LookLeft();
        }
    }

    void LookRight()
    {
        animator.transform.localScale = initialScale;
        speechBubbleRenderer.flipX = false;
        speechBubbleCanvas.localPosition = textPositionLeft;
    }

    void LookLeft()
    {
        animator.transform.localScale = Vector3.Scale(initialScale, reverseX);
        speechBubbleRenderer.flipX = true;
        speechBubbleCanvas.localPosition = textPositionRight;
    }
}
