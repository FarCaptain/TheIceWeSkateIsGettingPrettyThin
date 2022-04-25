using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelObjectTracker : MonoBehaviour
{
    [SerializeField] private int numFallableObjects;
    [SerializeField] public TextMeshProUGUI counter;
    [SerializeField] public TextMeshProUGUI total;
    [SerializeField] public GameObject endScreen;
    [SerializeField] public string nextLevel;
    [SerializeField] public float nextLevelLoadDelay;
    private string prefixString = "/";
    private float numLeft;
    private float numTotal;


    // Start is called before the first frame update
    void Start()
    {
        numFallableObjects = GameObject.FindGameObjectsWithTag("Fallable").Length;
        counter.text = numFallableObjects.ToString();
        numTotal = numFallableObjects;
        total.text = prefixString + numFallableObjects.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseNumFallableObjects()
    {
        numFallableObjects--;
        numLeft = numTotal - numFallableObjects;
        counter.text = numFallableObjects.ToString();

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
        endScreen.SetActive(true);
      
        Invoke("loadLevel", nextLevelLoadDelay);
        // Load Next level
    }

    private void loadLevel()
    {
        //Debug.Log("Switching levels");
        SceneManager.LoadScene(nextLevel);
    }

    public void OpenLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
