using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public PlayerControls controls;
    [SerializeField] GameObject quitMenu;

    private void Awake()
    {
        controls = new PlayerControls();
        Gameplay();
        controls.Gameplay.QuitGame.performed += ctx => QuitGame();
        quitMenu.SetActive(false);
    }

    void QuitGame()
    {
        controls.Gameplay.Disable();
        quitMenu.SetActive(true);
    }

    public void ReallyQuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        quitMenu.SetActive(false);
        controls.Gameplay.Enable();
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
