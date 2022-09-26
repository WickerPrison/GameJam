using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public PlayerControls controls;
    [SerializeField] GameObject quitMenu;
    [SerializeField] GameObject winScreen;
    PlayerScript playerScript;
    AudioSource source;
    float defaultVolume;

    private void Awake()
    {
        controls = new PlayerControls();
        Gameplay();
        controls.Gameplay.QuitGame.performed += ctx => QuitGame();
    }

    private void Start()
    {
        quitMenu.SetActive(false);
        winScreen.SetActive(false);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        source = GetComponent<AudioSource>();
        defaultVolume = source.volume;
    }

    public void YouWin()
    {
        winScreen.SetActive(true);
        playerScript.Win();
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

    public void ResumeMusic()
    {
        source.volume = defaultVolume;
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
