using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject helpObject;

    public GameObject glossaryObject;

    public GameObject pauseButtons;

    public bool isPaused;

    public bool inHelp;

    public bool inGlossary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);

        helpObject.SetActive(false);

        glossaryObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inHelp || inGlossary)
            {
                HideHelp();
                HideGlossary();
            }
            else if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void ShowHelp()
    {
        inHelp = true;
        helpObject.SetActive(true);
        pauseButtons.SetActive(false);
        pauseMenu.GetComponent<Image>().enabled = false;
    }

    public void HideHelp()
    {
        inHelp = false;
        helpObject.SetActive(false);
        pauseButtons.SetActive(true);
        pauseMenu.GetComponent<Image>().enabled = true;
    }

    public void ShowGlossary()
    {
        inGlossary = true;
        glossaryObject.SetActive(true);
    }

    public void HideGlossary()
    {
        inGlossary = false;
        glossaryObject.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
