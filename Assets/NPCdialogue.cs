using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCdialogue : MonoBehaviour
{
    [SerializeField] float talkDistance;
    PlayerControls controls;
    PlayerMovement playerScript;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        controls.Gameplay.Talk.performed += ctx => StartConversation();
    }

    void StartConversation()
    {
        if(Vector2.Distance(playerScript.transform.position, transform.position) > talkDistance)
        {
            return;
        }

        Debug.Log("Talk");
    }

    // Update is called once per frame
    void Update()
    {
        
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
