using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainHandler : MonoBehaviour
{
    public int xMax = 100;
    public int yMax = 100;

    public int chunkSize = 10;

    public Tile stone;

    private GameObject[,] chunks;

    private Grid grid;

    /*
     
    The world is divided into chunks

    The chunks are gameobjects

    Each chunk has 2 tilemaps as children, one for foreground
    one for background.

    This comment is pointless

     */
    


    void Start()
    {
        CreateChunks();
        CreateTerrain();
    }

    public GameObject GetChunk(int x, int y)
    {
        float chunkX= (Mathf.Round(x / chunkSize) * chunkSize);
        float chunkY = (Mathf.Round(y / chunkSize) * chunkSize);
        chunkY /= chunkSize;
        chunkX /= chunkSize;

        return chunks[(int) chunkX, (int) chunkY];
    }
    void PlaceTile(int x, int y, Tile tile, bool background=false)
    {
        GameObject chunk = GetChunk(x,y);
        GameObject obj;
        if (background)
        {
            obj = chunk.transform.Find("bg").gameObject;
        }
        else
        {
            obj = chunk.transform.Find("fg").gameObject;
        }

        Tilemap tilemap = obj.GetComponent<Tilemap>();
        tilemap.SetTile(new Vector3Int(x, y, 0), tile);

    }
    
    void CreateChunks()
    {
        int numChunksX = xMax / chunkSize;
        int numChunksY = yMax / chunkSize;
        chunks = new GameObject[numChunksX, numChunksY];
        for (int x = 0; x < numChunksX; x++)
        {
            for (int y = 0; y < numChunksY; y++)
            {
                GameObject chunk = new GameObject();
                chunk.name = "chunk_" + x.ToString() + "_" + y.ToString();
                chunk.transform.parent = transform;
                chunk.isStatic = true;

                GameObject fg = new GameObject { name = "fg" };
                fg.AddComponent<Tilemap>();
                fg.AddComponent<TilemapRenderer>();
                fg.GetComponent<TilemapRenderer>().sortingLayerName = "Tiles-FG";
                fg.AddComponent<TilemapCollider2D>();
                fg.transform.parent = chunk.transform;

                GameObject bg = new GameObject { name = "bg" };
                bg.AddComponent<Tilemap>();
                bg.AddComponent<TilemapRenderer>();
                bg.GetComponent<TilemapRenderer>().sortingLayerName = "Tiles-BG";
                bg.transform.parent = chunk.transform;

                chunks[x,y] = chunk;
            }
        }
    }

    void CreateTerrain()
    {
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < xMax; y++)
            {
                PlaceTile(x, y, stone);
            }
        }
    }
}