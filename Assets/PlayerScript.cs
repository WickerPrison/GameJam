using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    float moveDir;
    float walkSpeed = 10;
    public GameObject speechBubble;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Vector3 textPositionLeft;
    [SerializeField] Vector3 textPositionRight;
    [SerializeField] RectTransform speechBubbleCanvas;
    SpriteRenderer speechBubbleRenderer;
    public Animator animator;
    Vector3 initialScale;
    Vector3 reverseX = new Vector3(-1, 1, 1);
    InputManager im;
    public bool mysterySolved;
    public bool hasWon = false;
    PlayerDialogue playerDialogue;
    float timer = 10;

    // Start is called before the first frame update
    void Start()
    {
        speechBubbleRenderer = speechBubble.GetComponent<SpriteRenderer>();
        playerDialogue = GetComponent<PlayerDialogue>();

        im = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        im.controls.Gameplay.Walk.performed += ctx => moveDir = ctx.ReadValue<float>();
        im.controls.Gameplay.Walk.canceled += ctx => moveDir = 0;

        speechBubble.SetActive(false);

        animator = GetComponentInChildren<Animator>();
        initialScale = animator.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Walk();

        if(moveDir > 0)
        {
            LookRight();
        }
        else if(moveDir < 0)
        {
            LookLeft();
        }

        if (hasWon)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = Random.Range(5f, 15f);
                DeathLine();
            }
        }
    }

    public void DeathLine()
    {
        playerDialogue.StartConversation(playerDialogue.deathConversation);
    }

    public void SayLine(string line)
    {
        speechBubble.SetActive(true);
        dialogueText.text = line;
    }

    void Walk()
    {
        if(transform.position.x <= -1.8f && moveDir < 0)
        {
            moveDir = 0;
        }


        animator.SetFloat("MoveDir", Mathf.Abs(moveDir));

        transform.Translate(new Vector2(moveDir * Time.deltaTime * walkSpeed, 0));
    }

    public void Win()
    {
        hasWon = true;
        im.controls.Gameplay.Disable();
        animator.Play("Win");
    }

    public void LookLeft()
    {
        animator.transform.localScale = Vector3.Scale(initialScale, reverseX);
        speechBubbleCanvas.localPosition = textPositionRight;
        speechBubbleRenderer.flipX = true;
    }

    public void LookRight()
    {
        animator.transform.localScale = initialScale;
        speechBubbleCanvas.localPosition = textPositionLeft;
        speechBubbleRenderer.flipX = false;
    }
}
