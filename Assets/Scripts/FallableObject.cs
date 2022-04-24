using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallableObject : MonoBehaviour
{
    private GameObject grid;
    private IceManager iceManager;
    private int xLoc;
    private int yLoc;
    private int zLoc;
    private Vector3Int loc;

    private LevelObjectTracker tracker;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("IceManager");
        iceManager = grid.GetComponent<IceManager>();
        xLoc = Mathf.FloorToInt(transform.position.x);
        yLoc = Mathf.FloorToInt(transform.position.y);
        zLoc = Mathf.FloorToInt(transform.position.z);
        loc = new Vector3Int(xLoc, yLoc, zLoc);

        tracker = GameObject.Find("Tracker").GetComponent<LevelObjectTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iceManager.GetTileName(loc).Equals("Empty"))
        {
            tracker.decreaseNumFallableObjects();
            Destroy(this.gameObject);
        }
        
    }
}
