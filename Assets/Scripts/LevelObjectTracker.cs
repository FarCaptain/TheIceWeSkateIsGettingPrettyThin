using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectTracker : MonoBehaviour
{
    private int numFallableObjects;

    // Start is called before the first frame update
    void Start()
    {
        numFallableObjects = GameObject.FindGameObjectsWithTag("Fallable").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseNumFallableObjects()
    {
        numFallableObjects--;

        if (numFallableObjects == 0)
        {
            // trigger win transition
        }
    }

    public int getNumFallableObjects()
    {
        return numFallableObjects;
    }
}
