using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelObjectTracker : MonoBehaviour
{
    [SerializeField] private int numFallableObjects;
    [SerializeField] public Text counter;

    // Start is called before the first frame update
    void Start()
    {
        numFallableObjects = GameObject.FindGameObjectsWithTag("Fallable").Length;
        counter.text = numFallableObjects.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseNumFallableObjects()
    {
        numFallableObjects--;
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

    }
}
