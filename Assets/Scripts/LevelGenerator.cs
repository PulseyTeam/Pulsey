using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;

#endif


public class LevelGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;

    public int width;
    public int height;

    private void Start()
    {
        ClearMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (height > width)
            {
                var swapHeight = height;
                height = width;
                width = swapHeight;
            }

            ClearMap();
            GenerateMap(false);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (height < width)
            {
                var swapHeight = height;
                height = width;
                width = swapHeight;
            }

            ClearMap();
            GenerateMap(true);
        }
    }

    public void GenerateMap(bool sideWays)
    {
        ClearMap();
        int[,] map = new int[height, width];
        float seed;
        seed = Time.time;
        map = MapFunctions.GenerateArray(width, height, true);
        map = sideWays ? MapFunctions.PerlinNoiseSideWays(map, seed) : MapFunctions.PerlinNoise(map, seed);
        MapFunctions.RenderMap(map, tilemap, tile);
    }

    public void ClearMap()
    {
        tilemap.ClearAllTiles();
        for (int i = 0; i <= 10; ++i)
        {
            tilemap.SetTile(new Vector3Int(i, height + 1, 0), tile);
        }
    }
}