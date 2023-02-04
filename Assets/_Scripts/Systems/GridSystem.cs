using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public const int xSize = 40, ySize = 4;
    public float tileSize = 0.6f;
    //public int halfX, halfY;
    public GameObject[][] tiles = new GameObject[ySize][];
    public GameObject tile;

    GameObject CreateTile(Vector3 tilePosition)
    {
        //GameObject go = new();
        GameObject go = Instantiate(tile);
        go.name = "Tile(X:" + tilePosition.x + ",Y:" + tilePosition.y + ")";
        go.transform.position = new Vector3(tilePosition.x, tilePosition.y, 1);
        return go;
    }
    void GenerateMap()
    {
        GameObject world = new();
        // Create map
        for (int y = 0; y < ySize; y++)
        {
            tiles[y] = new GameObject[xSize];
            for (int x = 0; x < xSize; x++)
            {
                // Tile get realworld position, other tileAddOn use this
                GameObject tile = CreateTile(new Vector3(x * tileSize, y * tileSize, 1));
                tile.transform.parent = world.transform;
                
                tiles[y][x] = tile;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Destroy(GetTile());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetNeighbors();
        }
    }
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
