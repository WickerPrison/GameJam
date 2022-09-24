using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        Gameplay();
    }

    public void Gameplay()
    {
        controls.Dialogue.Disable();
        controls.Gameplay.Enable();
    }

    public void Dialogue()
    {
        controls.Gameplay.Disable();
        controls.Dialogue.Enable();
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
