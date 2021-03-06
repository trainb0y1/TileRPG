using UnityEngine;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{
    public TerrainHandler terrain;
    public Transform playerTransform;

    private List<GameObject> backgroundObjects;
    private Background[] backgrounds;

    void Start()
    {
        backgrounds = new Background[0];
        backgroundObjects = new List<GameObject>();
        InvokeRepeating("SwitchBackground", 1f, 1);
        SwitchBackground();
    }


    void SwitchBackground()
    {
        // Check for the current biome background, and if it's different,
        // fade the new one in

        // TEMPORARY
        // TODO: actually fade stuff
        
        if (terrain.GetBiome(terrain.GetChunkObject((int)playerTransform.position.x, (int)playerTransform.position.y)).backgrounds != backgrounds)
        {
            backgrounds = terrain.GetBiome(terrain.GetChunkObject((int)playerTransform.position.x, (int)playerTransform.position.y)).backgrounds;
            foreach (GameObject obj in backgroundObjects) {
                Destroy(obj);
                //backgroundObjects.Remove(obj);
            }

            foreach (Background background in backgrounds)
            {
                CreateBackground(background);
            }
        }
    }

    void CreateBackground(Background template)
    {
        GameObject obj = new GameObject();
        obj.name = "background_" + template.name;
        obj.transform.parent = transform;
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = template.sprite;
        renderer.sortingOrder = template.sortingOrder;
        renderer.sortingLayerName = "Backgrounds";
        renderer.drawMode = SpriteDrawMode.Tiled;

        if (template.infiniteX){
            renderer.size = new Vector2(renderer.size.x * 3, renderer.size.y);
        }
        if (template.infiniteY)
        {
            renderer.size = new Vector2(renderer.size.x, renderer.size.y * 3);
        }
        
        

        

        BackgroundScript script = obj.AddComponent<BackgroundScript>();
        script.paralaxEffectMultiplier = template.parralaxMultiplier;
        script.infiniteX = template.infiniteX;
        script.infiniteY = template.infiniteY;

        obj.transform.position += template.offset + new Vector3(0,terrain.world.worldGenHeight,0);

        backgroundObjects.Add(obj);

    }
}
