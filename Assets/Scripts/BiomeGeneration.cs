using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGeneration : MonoBehaviour {
    //these gameobjects will be the specific prefabs for the objects that will exist within the world(e.g trees, boulders, ponds, etc.)
    public GameObject terrainAsset1;
    public GameObject terrainAsset2;
    public GameObject terrainAsset3;

    //these floats will determine how many of each asset will populate the world upon the creation of the individual biome
    public float amountOfAsset1;
    public float amountOfAsset2;
    public float amountOfAsset3;

    Vector3 spawnPosition;
    bool worldPopulated = false;

    // Use this for initialization
    void Start () {
        //this will ensure that the biome has a randomly picked color upon activation
        this.GetComponent<SpriteRenderer>().material.color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
        //this for statement will populate the world accordingly with how mnay of a specific asset you want at randomized locations with 
        //randomized size as well.
        for (int i = 0; i < amountOfAsset1; i++)
        {
            spawnPosition = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), -1);
            GameObject biome = (GameObject)Instantiate(terrainAsset1, spawnPosition, Quaternion.identity);
            biome.transform.localScale = new Vector3(Random.Range(1, 5), Random.Range(1, 5), 1);
            biome.transform.SetParent(this.transform);
            biome.transform.localPosition = spawnPosition;
            
        }
        for (int i = 0; i < amountOfAsset2; i++)
        {
            spawnPosition = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), -1);
            GameObject biome = (GameObject)Instantiate(terrainAsset2, spawnPosition, Quaternion.identity);
            biome.transform.localScale = new Vector3(Random.Range(1, 5), Random.Range(1, 5), 1);
            biome.transform.SetParent(this.transform);
            biome.transform.localPosition = spawnPosition;

        }
        for (int i = 0; i < amountOfAsset3; i++)
        {
            spawnPosition = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), -1);
            GameObject biome = (GameObject)Instantiate(terrainAsset3, spawnPosition, Quaternion.identity);
            biome.transform.localScale = new Vector3(Random.Range(1, 5), Random.Range(1, 5), 1);
            biome.transform.SetParent(this.transform);
            biome.transform.localPosition = spawnPosition;

        }
    }

    // Update is called once per frame
    void Update() { 
    }
}
