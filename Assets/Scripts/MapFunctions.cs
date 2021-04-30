using UnityEngine;
using UnityEngine.Tilemaps;

public class MapFunctions
{
    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];

        for (int i = 0; i < map.GetUpperBound(0); ++i)
        {
            for (int j = 0; j < map.GetUpperBound(1); ++j)
            {
                if (empty)
                {
                    map[i, j] = 0;
                }
                else
                {
                    map[i, j] = 1;
                }
            }
        }

        return map;
    }

    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles();

        for (int i = 0; i < map.GetUpperBound(0); ++i)
        {
            for (int j = 0; j < map.GetUpperBound(1); ++j)
            {
                if (map[i, j] == 1)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), tile);
                }
            }
        }
    }

    public static void RenderMapWithOffset(int[,] map, Tilemap tilemap, TileBase tile, Vector2Int offset)
    {
        for (int i = 0; i < map.GetUpperBound(0); ++i)
        {
            for (int j = 0; j < map.GetUpperBound(1); ++j)
            {
                if (map[i, j] == 1)
                {
                    tilemap.SetTile(new Vector3Int(i + offset.x, j + offset.y, 0), tile);
                }
            }
        }
    }

    public static void UpdateMap(int[,] map, Tilemap tilemap)
    {
        for (int i = 0; i < map.GetUpperBound(0); ++i)
        {
            for (int j = 0; j < map.GetUpperBound(1); ++j)
            {
                if (map[i, j] == 0)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), null);
                }
            }
        }
    }

    public static int[,] PerlinNoise(int[,] map, float seed)
    {
        int newPoint;

        float reduction = 0.5f;

        for (int i = 0; i < map.GetUpperBound(0); ++i)
        {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(i, seed) - reduction) * map.GetUpperBound(1));
            newPoint += (map.GetUpperBound(1) / 2);
            for (int j = newPoint; j >= 0; --j)
            {
                map[i, j] = 1;
            }
        }

        return map;
    }

    public static int[,] PerlinNoiseSideWays(int[,] map, float seed)
    {
        int newPoint;

        float reduction = 0.5f;

        for (int i = 0; i < map.GetUpperBound(1); ++i)
        {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(i, seed) - reduction) * (map.GetUpperBound(0) / 5 * 2));
            newPoint += map.GetUpperBound(0) / 5 * 2;


            for (int j = newPoint; j >= 0; --j)
            {
                map[j, i] = 1;
            }

            for (int j = newPoint + map.GetUpperBound(0) / 5; j < map.GetUpperBound(0); ++j)
            {
                map[j, i] = 1;
            }
        }

        return map;
    }
}