using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{   
    //  Set size
    const int xSize = 275 /4, ySize = 105 / 4;
    const int bla = ySize / 5;
    float tileSize = 0.15f * 4;
    //public int halfX, halfY;
    public GameObject[][] tiles = new GameObject[ySize][];
    public GameObject tile;
    private Master master;

    // 1 to 100%
    const float avgChange = 10;
    float rockSmall = avgChange + 10;
    float rockBig = avgChange + 1;
    float waterBigChance = avgChange + 1;
    float smallWaterChance = avgChange + 20;
    float nitrogenChance = avgChange + 20;
    float decreaseChangeForResource = 3;

    GameObject CreateTile(Vector3 tilePosition)
    {
        GameObject go = Instantiate(tile);
        go.name = "Tile(X:" + tilePosition.x + ",Y:" + tilePosition.y + ")";
        go.transform.position = new Vector3(tilePosition.x, tilePosition.y, 1);
        return go;
    }
    public void GenerateMap(bool spawnRooks = false)
    {
        GameObject world = new();

        
        bool hasIncreasedSpawingRate = false;
        // Create map
        for (int y = 0; y < ySize; y++)
        {
            if ((y % bla) == 1)
            {
                hasIncreasedSpawingRate = false;
            }
            if (hasIncreasedSpawingRate == false && (y % 5) == 0)
            {
                rockSmall -= decreaseChangeForResource * 1.0f;
                rockBig -= decreaseChangeForResource * 1.5f;
                waterBigChance -= decreaseChangeForResource * 1.0f;
                smallWaterChance -= decreaseChangeForResource * 0.7f;
                nitrogenChance -= decreaseChangeForResource * 0.5f; ;
                hasIncreasedSpawingRate = true;
            }
            
            tiles[y] = new GameObject[xSize];
            for (int x = 0; x < xSize; x++)
            {
               


                // MainTile: Tile get realworld position, other tileAddOn use this
                GameObject tile = CreateTile(new Vector3(x * tileSize, y * tileSize, 1));
                tile.transform.parent = world.transform;
                tiles[y][x] = tile;

                // Children
                int tileType = 0;
                float tmpTileSize = tileSize;

                // Rock Big
                if (spawnRooks && Random.Range(0, 100) <= rockBig)
                {
                    tmpTileSize = tileSize * 4;
                    tileType = 0;
                }
                // Rock small
                else if (Random.Range(0, 100) <= rockSmall)
                {
                    tmpTileSize = tileSize * 2;
                    tileType = 0;
                }
                // water
                else if (Random.Range(0, 100) <= waterBigChance)
                {
                    tmpTileSize = tileSize * Random.Range(1, 3);
                    tileType = 1;
                }
                // small water
                else if (Random.Range(0, 100) <= smallWaterChance)
                {
                    tileType = 2;
                }
                // nitrogen
                else if (Random.Range(0, 100) <= nitrogenChance)
                {
                    tileType = 3;
                }
                else 
                {
                    // Nothing
                    tileType = -1;
                }
                if (tileType != -1)
                {
                    GameObject go = Instantiate(master.resources[tileType], tile.transform.position, Quaternion.identity);
                    go.transform.localScale *= tmpTileSize;
                    go.transform.parent = tile.transform;
                }

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        //GenerateMap(true);
    }


    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        Destroy(GetTile());
    //    }
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        GetNeighbors();
    //    }
    //}
    GameObject GetTile()
    {
        Vector2Int tilePosition = GetTilePostionInWorld();
        if (ExistTile(tilePosition))
        {
            return tiles[tilePosition.y][tilePosition.x];
        }
        return null;
    }
    GameObject GetTile(Vector2Int tilePosition)
    {
        if (ExistTile(tilePosition))
        {
            return tiles[tilePosition.y][tilePosition.x];
        }
        return null;
    }
    bool ExistTile(Vector2Int tilePosition)
    {
        if (
            tilePosition.x >= 0 && tilePosition.x < xSize &&
            tilePosition.y >= 0 && tilePosition.y < ySize &&
            tiles[tilePosition.y] != null &&
            tiles[tilePosition.y][tilePosition.x] != null &&
            tiles[tilePosition.y][tilePosition.x].gameObject != null)
        {
            return true;
        }
        return false;
    }
    Vector2Int GetTilePostionInWorld()
    {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) / tileSize;
        Vector2Int position = new(Mathf.RoundToInt(cameraPosition.x)  , 
            Mathf.RoundToInt(cameraPosition.y ) );
        return position;
    }
    List<GameObject> GetNeighbors(int _checkArea = 3)
    {
        List<GameObject> neighbors = new();
        Vector2Int tilePosition = GetTilePostionInWorld();
        if (((_checkArea % 3) == 0) && _checkArea <= 12 && ExistTile(tilePosition))
        {
            _checkArea = _checkArea == 3 ? 1 : _checkArea == 6 ? 2 : _checkArea == 9 ? 3 : _checkArea == 12 ? 4 : 0;
            for (int y = -_checkArea; y < _checkArea + 1; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    Vector2Int neighborsPosition = new(tilePosition.x + x, tilePosition.y + y);
                    if ((y == 0 && x == 0) == false &&
                        ExistTile(neighborsPosition))
                    {
                        neighbors.Add(GetTile(neighborsPosition));
                    }
                }
            }
        }
        return neighbors;
    }
}
