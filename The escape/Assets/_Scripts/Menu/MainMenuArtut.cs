using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuArtut : MonoBehaviour
{
    public void ClickBacktoDefaultSceneButton()
    {
        SceneManager.LoadScene("ArturTestScene");
    }

}
