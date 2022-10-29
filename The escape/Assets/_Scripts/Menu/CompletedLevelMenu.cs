using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletedLevelMenu : MonoBehaviour
{
    private bool isActiveOptionsButton = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject completedLevelPanel;

    public GameObject Player;
    private bool isPlayerCompleteLvl = false;
    public void Start()
    {
        optionsPanel.SetActive(false);
        completedLevelPanel.SetActive(false);
    }
    public void Update()
    {
        if (Player.gameObject.transform.position.x > 24)
        {
            isPlayerCompleteLvl = !isPlayerCompleteLvl;
            if (isPlayerCompleteLvl)
            {
                completedLevelPanel.SetActive(true);
            }
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


    public void ClickOptionsButton()
    {
        isActiveOptionsButton = !isActiveOptionsButton;
        pausePanel.SetActive(false);
    }
    public void ClickBackOptionButton()
    {
        pausePanel.SetActive(true);

    }
    public void ClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ClickNextLevelButton()
    {
        Debug.Log("NextLevelButton is clicked");
    }
    public void ClickBacktoMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
