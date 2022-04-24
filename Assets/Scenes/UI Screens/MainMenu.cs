using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private string startLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
