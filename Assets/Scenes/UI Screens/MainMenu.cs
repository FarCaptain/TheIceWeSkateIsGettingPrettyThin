using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string startLevel;
    [SerializeField] private string levelSelect;

    public void StartGame()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
