using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    float moveDir;
    [SerializeField] float walkSpeed;
    public GameObject speechBubble;
    [SerializeField] TextMeshProUGUI dialogueText;
    Animator animator;
    Vector3 initialScale;
    Vector3 speechBubblePosition;
    Vector3 reverseX = new Vector3(-1, 1, 1);
    InputManager im;

    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        im.controls.Gameplay.Walk.performed += ctx => moveDir = ctx.ReadValue<float>();
        im.controls.Gameplay.Walk.canceled += ctx => moveDir = 0;

        speechBubblePosition = speechBubble.transform.localPosition;
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

    public void LookLeft()
    {
        animator.transform.localScale = Vector3.Scale(initialScale, reverseX);
        speechBubble.transform.localPosition = Vector3.Scale(speechBubblePosition, reverseX);
    }

    public void LookRight()
    {
        animator.transform.localScale = initialScale;
        speechBubble.transform.localPosition = speechBubblePosition;
    }
}
