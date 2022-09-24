using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] GameObject examineMessage;
    InputManager im;
    float interactDistance = 10;
    bool hasInspected = false;
    PlayerScript playerScript;
    PlayerDialogue playerDialogue;
    float playerDistance;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        playerDialogue = playerScript.gameObject.GetComponent<PlayerDialogue>();
        im = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        im.controls.Gameplay.Interact.performed += ctx => InspectMap();
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(playerScript.transform.position, transform.position);
        if (!hasInspected && playerDistance < interactDistance)
        {
            examineMessage.SetActive(true);
        }
        else
        {
            examineMessage.SetActive(false);
        }
    }

    void InspectMap()
    {
        if (hasInspected || playerDistance > interactDistance)
        {
            return;
        }

        playerDialogue.StartConversation(playerDialogue.mapConversation);
    }
}
