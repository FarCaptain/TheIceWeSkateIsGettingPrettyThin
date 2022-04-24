using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelObjectTracker : MonoBehaviour
{
    [SerializeField] private int numFallableObjects;
    [SerializeField] public TextMeshProUGUI counter;
    [SerializeField] public TextMeshProUGUI endScreen;
    [SerializeField] public string nextLevel;
    [SerializeField] public float nextLevelLoadDelay;
    private string prefixString = "Remaining: ";


    // Start is called before the first frame update
    void Start()
    {
        numFallableObjects = GameObject.FindGameObjectsWithTag("Fallable").Length;
        counter.text = prefixString + numFallableObjects.ToString();
        endScreen.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseNumFallableObjects()
    {
        numFallableObjects--;
        counter.text = prefixString + numFallableObjects.ToString();

        if (numFallableObjects == 0)
        {
            WinTransition();
        }
    }

    public int getNumFallableObjects()
    {
        return numFallableObjects;
    }

    private void WinTransition()
    {
        endScreen.text = "You Win";
        endScreen.alpha = 1;
        Invoke("loadLevel", nextLevelLoadDelay);
        // Load Next level
    }

    private void loadLevel()
    {
        Debug.Log("Switching levels");
        SceneManager.LoadScene(nextLevel);
    }
}
