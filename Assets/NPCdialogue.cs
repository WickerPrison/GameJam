using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCdialogue : MonoBehaviour
{
    [SerializeField] float talkDistance;
    [SerializeField] GameObject talkMessage;
    public GameObject speechBubble;
    public TextMeshProUGUI dialogueText;
    public PlayerControls controls;
    public PlayerScript playerScript;
    public bool isTalking = false;
    string dialogueWords = "This is my dialogue";
    Vector3 initialScale;
    [SerializeField] Animator animator;
    Vector3 textBubblePosition;
    Vector3 reverseX = new Vector3(-1, 1, 1);

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Dialogue.Disable();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        speechBubble.SetActive(false);
        controls.Gameplay.Interact.performed += ctx => StartConversation();
        controls.Dialogue.Disable();
        initialScale = animator.transform.localScale;
        textBubblePosition = speechBubble.transform.localPosition;
    }

    void StartConversation()
    {
        if(Vector2.Distance(playerScript.transform.position, transform.position) > talkDistance)
        {
            return;
        }


        controls.Gameplay.Disable();
        controls.Dialogue.Enable();
        playerScript.controls.Gameplay.Disable();
        playerScript.controls.Dialogue.Enable();
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
            animator.transform.localScale = initialScale;
            speechBubble.transform.localPosition = textBubblePosition;
        }
        else if (playerScript.transform.position.x < transform.position.x)
        {
            animator.transform.localScale = Vector3.Scale(initialScale, reverseX);
            speechBubble.transform.localPosition = Vector3.Scale(textBubblePosition, reverseX);
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
