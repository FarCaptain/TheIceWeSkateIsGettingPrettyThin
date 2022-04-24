using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void OpenLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
