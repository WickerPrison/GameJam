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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(moveDir * Time.deltaTime * walkSpeed, 0));
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
