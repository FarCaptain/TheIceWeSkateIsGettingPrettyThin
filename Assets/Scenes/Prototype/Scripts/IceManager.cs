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
    private TileBase[] Wall;
    [SerializeField]
    private TileBase[] TopLeftCorner;
    [SerializeField]
    private TileBase[] TopSide;
    [SerializeField]
    private TileBase[] TopRightCorner;
    [SerializeField]
    private TileBase[] LeftSide;
    [SerializeField]
    private TileBase[] Center;
    [SerializeField]
    private TileBase[] RightSide;
    [SerializeField]
    private TileBase[] BottomLeftCorner;
    [SerializeField]
    private TileBase[] BottomSide;
    [SerializeField]
    private TileBase[] BottomRightCorner;
    [SerializeField]
    private TileBase[] TopLeftTiny;
    [SerializeField]
    private TileBase[] TopRightTiny;
    [SerializeField]
    private TileBase[] BottomLeftTiny;
    [SerializeField]
    private TileBase[] BottomRightTiny;


    //This dictionary and array are used to point to the next tile that should be displayed. I assume there is a better way to do this but I couldn't find one.
    private Dictionary<string, Vector2Int> tileIndex;
    private TileBase[][] allTiles;

    private Dictionary<Vector3Int, bool> visitTable;


    private Vector3Int currentTile;
    private Grid grid;

    private int[] dx = { 1, -1, 0, 0, -1, 1, -1, 1};
    private int[] dy = { 0, 0, -1, 1, -1, -1, 1, 1};

    // Start is called before the first frame update
    void Start()
    {
        tileIndex = new Dictionary<string, Vector2Int>();
        visitTable = new Dictionary<Vector3Int, bool>();

        allTiles = new TileBase[][] { Wall, TopLeftCorner, TopSide, TopRightCorner, 
                                            LeftSide, Center, RightSide, 
                                            BottomLeftCorner, BottomSide, BottomRightCorner, 
                                            TopLeftTiny, TopRightTiny, BottomLeftTiny, BottomRightTiny};

        // link list this isnt
        for (int i = 0; i < allTiles.Length; i++)
        {
            for (int j = 0; j < allTiles[i].Length; j++)
            {
                tileIndex.Add(allTiles[i][j].name, new Vector2Int(i, j));
            }
        }
        //tileIndex[allTiles[12].name] = -1;

        // if we are not using all tiles, choose from back of the array
        int startIndex = 12 - iceLayers + 1;
        //tileIndex[allTiles[0].name] = startIndex;

        grid = iceMap.layoutGrid;
        currentTile = grid.WorldToCell(Skater.transform.position);

        //test
        print("size:" + iceMap.size.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Skater != null)
        {
            if (Skater.GetComponent<MoveSkater>().isGliding)
                return;

            Vector3Int cellLocation = grid.WorldToCell(Skater.transform.position);

            if (iceMap.HasTile(cellLocation) && currentTile != cellLocation)
            {
                Vector2Int indexOfCurrentTile = tileIndex[iceMap.GetTile(currentTile).name];
                if (indexOfCurrentTile.y == 2 && !IsWall(currentTile))
                {
                    //Player Drowns
                    // TODO. Death animation
                    Skater.GetComponentInChildren<Animator>().SetBool("DeathTrigger", true);
                    Skater.GetComponent<MoveSkater>().hasFell = true;
                    //Object.Destroy(Skater, 0.1f);
                }
                else
                {
                    if (!IsWall(currentTile))
                        iceMap.SetTile(currentTile, allTiles[indexOfCurrentTile.x][indexOfCurrentTile.y + 1]);
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
                                    string tilename = iceMap.GetTile(tile).name;
                                    if (iceMap.HasTile(tile) && !IsWall(tile))
                                    {                                       
                                        iceMap.SetTile(tile, allTiles[tileIndex[tilename].x][2]);
                                    }                                       
                                }
                            }
                        }
                    }
                }
                currentTile = cellLocation;
            }
        }
    }

    private bool IsWall(Vector3Int tile)
    {
        string tilename = iceMap.GetTile(tile).name;
        return tileIndex[tilename].x == 0;
    }

    private bool IsWater(Vector3Int tile)
    {
        int indexOfNextTile = tileIndex[iceMap.GetTile(tile).name].y;
        return indexOfNextTile == 2;
    }

    private bool IsSteped(Vector3Int tile)
    {
        return tileIndex[iceMap.GetTile(tile).name].y == 1;
    }

    public string GetTileName(Vector3Int tilePos)
    {
        if (!iceMap.HasTile(tilePos))
            return "Empty";
        else if (IsWater(tilePos))
        {
            return "Water";
        }
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
