using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public Image helpImage;

    public Image glossaryImage;

    public bool isPaused;

    public bool inHelp;

    public bool inGlossary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);

        helpImage.gameObject.SetActive(false);

        glossaryImage.gameObject.SetActive(false);
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
        helpImage.gameObject.SetActive(true);
    }

    public void HideHelp()
    {
        inHelp = false;
        helpImage.gameObject.SetActive(false);
    }

    public void ShowGlossary()
    {
        inGlossary = true;
        glossaryImage.gameObject .SetActive(true);
    }

    public void HideGlossary()
    {
        inGlossary = false;
        glossaryImage .gameObject .SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
