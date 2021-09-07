using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class Ore: ScriptableObject
{
    public Tile ore;
    [Range(0, 1)]
    public float rarity;
    [Range(0, 1)]
    public float size;
    public int maxSpawnHeight;
    public Texture2D spreadTexture;
}
