using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IceManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap iceMap;
    [SerializeField]
    private GameObject Skater;
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
    [SerializeField]
    private TileBase tile13;

    //This dictionary and array are used to point to the next tile that should be displayed. I assume there is a better way to do this but I couldn't find one.
    private Dictionary<string, int> nextTile;
    private TileBase[] allTiles;


    private Vector3Int currentTile;
    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        nextTile = new Dictionary<string, int>();
        nextTile[tile1.name] = 1;
        nextTile[tile2.name] = 2;
        nextTile[tile3.name] = 3;
        nextTile[tile4.name] = 4;
        nextTile[tile5.name] = 5;
        nextTile[tile6.name] = 6;
        nextTile[tile7.name] = 7;
        nextTile[tile8.name] = 8;
        nextTile[tile9.name] = 9;
        nextTile[tile10.name] = 10;
        nextTile[tile11.name] = 11;
        nextTile[tile12.name] = 12;
        nextTile[tile13.name] = -1;

        allTiles = new TileBase[] { tile1, tile2, tile3, tile4, tile5, tile6, tile7, tile8, tile9, tile10, tile11, tile12, tile13 };
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
