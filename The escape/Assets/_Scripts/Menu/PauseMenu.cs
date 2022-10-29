using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isActivePauseButton = false;
    private bool isActiveOptionsButton = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;

    private void Update()
    {
        if (isActivePauseButton)
        {
            pausePanel.SetActive(isActivePauseButton);
        }
        else
        {
            pausePanel.SetActive(isActivePauseButton);
        }

        if (isActiveOptionsButton)
        {
            optionsPanel.SetActive(isActiveOptionsButton);
        }
        else
        {
            optionsPanel.SetActive(isActiveOptionsButton);
        }
    }

    public void ClickPauseButton()
    {
        isActivePauseButton = !isActivePauseButton;  
    }
    public void ClickOptionsButton()
    {
        isActiveOptionsButton = !isActiveOptionsButton;
    }
    public void ClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ClickLastCheckpointButton()
    {
        Debug.Log("LastCheckpointButton is clicked");
    }
    public void ClickBacktoMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ClickBacktoDefaultSceneButton()
    {
        SceneManager.LoadScene("ArturTestScene");

    }
}
