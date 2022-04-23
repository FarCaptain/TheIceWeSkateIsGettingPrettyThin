using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IceManager : MonoBehaviour
{
    [Range(2, 12)]
    public int iceLayers = 2;

    [SerializeField]
    private Tilemap iceMap;
    [SerializeField]
    private GameObject Skater;
    [SerializeField]
    private TileBase tile0;
    [SerializeField]
    private TileBase tile1;
    [SerializeField]
    private TileBase tile2;
    [SerializeField]
    private TileBase tile3;
    [SerializeField]
    private TileBase tile4;
    [SerializeField]
    private TileBase tile5;
    [SerializeField]
    private TileBase tile6;
    [SerializeField]
    private TileBase tile7;
    [SerializeField]
    private TileBase tile8;
    [SerializeField]
    private TileBase tile9;
    [SerializeField]
    private TileBase tile10;
    [SerializeField]
    private TileBase tile11;
    [SerializeField]
    private TileBase tile12;


    //This dictionary and array are used to point to the next tile that should be displayed. I assume there is a better way to do this but I couldn't find one.
    private Dictionary<string, int> nextTile;
    private TileBase[] allTiles;


    private Vector3Int currentTile;
    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        nextTile = new Dictionary<string, int>();

        allTiles = new TileBase[] { tile0, tile1, tile2, tile3, tile4, tile5, tile6, tile7, tile8, tile9, tile10, tile11, tile12 };

        // link list this is
        for (int i = 0; i < 12; i++)
        {
            nextTile[allTiles[i].name] = (i + 1);
        }
        nextTile[allTiles[12].name] = -1;

        // if we are not using all tiles, choose from back of the array
        int startIndex = 12 - iceLayers + 1;
        nextTile[allTiles[0].name] = startIndex;

        grid = iceMap.layoutGrid;
        currentTile = grid.WorldToCell(Skater.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int cellLocation = grid.WorldToCell(Skater.transform.position);
        if (iceMap.HasTile(cellLocation) && currentTile != cellLocation)
        {
            int indexOfNextTile = nextTile[iceMap.GetTile(currentTile).name];
            if (indexOfNextTile == -1)
            {
                //Player Drowns
                Object.Destroy(Skater);
            }
            else
            {
                iceMap.SetTile(currentTile, allTiles[indexOfNextTile]);
            }
            currentTile = cellLocation;
        }
    }
}
