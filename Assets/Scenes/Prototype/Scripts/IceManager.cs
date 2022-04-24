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

    private Dictionary<Vector3Int, bool> visitTable;


    private Vector3Int currentTile;
    private Grid grid;

    private int[] dx = { 1, -1, 0, 0, -1, 1, -1, 1};
    private int[] dy = { 0, 0, -1, 1, -1, -1, 1, 1};

    // Start is called before the first frame update
    void Start()
    {
        nextTile = new Dictionary<string, int>();
        visitTable = new Dictionary<Vector3Int, bool>();

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

        //test
        print("size:" + iceMap.size.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Skater.GetComponent<MoveSkater>().isGliding)
            return;

        Vector3Int cellLocation = grid.WorldToCell(Skater.transform.position);

        if (iceMap.HasTile(cellLocation) && currentTile != cellLocation)
        {
            int indexOfNextTile = nextTile[iceMap.GetTile(currentTile).name];
            if (indexOfNextTile == -1)
            {
                //Player Drowns
                // TODO. Death animation
                Object.Destroy(Skater);
            }
            else
            {
                iceMap.SetTile(currentTile, allTiles[indexOfNextTile]);
                for (int i = 0; i < 4; i++)
                {
                    Vector3Int adjacentTile = new Vector3Int(currentTile.x + dx[i], currentTile.y + dy[i], currentTile.z);

                    //if(iceMap.HasTile(adjacentTile))
                    //iceMap.SetTile(adjacentTile, tile6);

                    if (iceMap.HasTile(adjacentTile) && !IsSteped(adjacentTile))
                    {
                        visitTable.Clear();
                        if (findEncirledGrids(adjacentTile))
                        {
                            foreach (var pair in visitTable)
                            {
                                //set to water tiles
                                Vector3Int tile = pair.Key;
                                if (iceMap.HasTile(tile))
                                    iceMap.SetTile(tile, allTiles[allTiles.Length - 1]);
                            }
                        }
                    }
                }
            }
            currentTile = cellLocation;
        }
    }

    private bool IsWater(Vector3Int tile)
    {
        int indexOfNextTile = nextTile[iceMap.GetTile(tile).name];
        return indexOfNextTile == -1;
    }

    private bool IsSteped(Vector3Int tile)
    {
        return iceMap.GetTile(tile).name != allTiles[0].name;
    }

    public string GetTileName(Vector3Int tilePos)
    {
        if (!iceMap.HasTile(tilePos))
            return "Empty";
        return iceMap.GetTile(tilePos).name;
    }

    //dfs
    private bool findEncirledGrids(Vector3Int tile)
    {

        if (!iceMap.HasTile(tile))
            return false;

        if (IsSteped(tile))
        {
            if (visitTable.ContainsKey(tile))
                visitTable.Remove(tile);
            return true;
        }

        visitTable[tile] = true;

        for (int i = 0; i < 4; i++)
        {
            Vector3Int adjacentTile = new Vector3Int(tile.x + dx[i], tile.y + dy[i], tile.z);

            if (visitTable.ContainsKey(adjacentTile))
                continue;

            if (findEncirledGrids(adjacentTile) == false)
                return false;
        }
        return true;
    }
}
