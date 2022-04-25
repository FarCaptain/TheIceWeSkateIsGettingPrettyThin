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
    private bool isActive = true;
    [SerializeField] Tilemap iceMap;

    private LevelObjectTracker tracker;
    private Transform skater;

    // Start is called before the first frame update
    void Start()
    {
        iceManager = GameObject.Find("IceManager").GetComponent<IceManager>();
        grid = iceMap.layoutGrid;
        loc = grid.WorldToCell(transform.position);


        tracker = GameObject.Find("Tracker").GetComponent<LevelObjectTracker>();
        skater = GameObject.Find("Skater").transform;

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((iceManager.GetTileName(loc).Equals("Water")) && isActive)
        {
            tracker.decreaseNumFallableObjects();
            anim.SetBool("FallBool", true);
            isActive = false;
        }
        else if(isActive)
        {
            Vector3 point2Skater = skater.position - transform.position;
            point2Skater.Normalize();
            float rot_z = Mathf.Atan2(point2Skater.x, point2Skater.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rot_z + 180, -Vector3.forward);
        }
    }
}
