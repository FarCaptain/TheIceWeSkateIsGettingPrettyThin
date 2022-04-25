using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallableObject : MonoBehaviour
{
    private Grid grid;
    private IceManager iceManager;
    private Vector3Int loc;
    private Animator anim;
    [SerializeField] Tilemap iceMap;

    private LevelObjectTracker tracker;

    // Start is called before the first frame update
    void Start()
    {
        iceManager = GameObject.Find("IceManager").GetComponent<IceManager>();
        grid = iceMap.layoutGrid;
        loc = grid.WorldToCell(transform.position);


        tracker = GameObject.Find("Tracker").GetComponent<LevelObjectTracker>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iceManager.GetTileName(loc).Equals("Water"))
        {
            tracker.decreaseNumFallableObjects();
            anim.SetTrigger("FallTrigger");
        }

    }
}
