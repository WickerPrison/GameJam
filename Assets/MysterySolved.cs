using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysterySolved : MonoBehaviour
{
    PlayerScript playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScript.mysterySolved = true;
    }
}
