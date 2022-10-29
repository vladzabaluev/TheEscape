using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private bool isActiveOptionsButton = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject gameOverPanel;

    public GameObject Player;
    private bool isPlayerDead = false;
    public void Start()
    {
        optionsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    public void Update()
    {
        if (Player.gameObject.transform.position.y < -10)
        {
            isPlayerDead = !isPlayerDead;
            if (isPlayerDead)
            {
                gameOverPanel.SetActive(true);
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
    public void ClickLastCheckpointButton()
    {
        Debug.Log("LastCheckpointButton is clicked");
    }
    public void ClickBacktoMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
