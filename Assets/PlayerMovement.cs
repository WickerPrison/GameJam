using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;
    float moveDir;
    [SerializeField] float walkSpeed;
    [SerializeField] GameObject speechBubble;
    [SerializeField] TextMeshProUGUI dialogueText;
    Animator animator;
    Vector3 initialScale;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        controls.Gameplay.Walk.performed += ctx => moveDir = ctx.ReadValue<float>();
        controls.Gameplay.Walk.canceled += ctx => moveDir = 0;

        speechBubble.SetActive(false);

        animator = GetComponentInChildren<Animator>();
        initialScale = animator.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("MoveDir", Mathf.Abs(moveDir));
        transform.Translate(new Vector2(moveDir * Time.deltaTime * walkSpeed, 0));

        if(moveDir > 0)
        {
            animator.transform.localScale = initialScale;
        }
        else if(moveDir < 0)
        {
            animator.transform.localScale = Vector3.Scale(initialScale, new Vector3(-1, 1, 1));
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
