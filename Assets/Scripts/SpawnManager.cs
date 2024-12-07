using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Terrain terrain;
    public List<Origami> origamiFigures;
    public List<GoldPile> otherRewards;
    public KeyChest keyChest;
    public int otherRewardsCount;
    TerrainData terrainData;
    Vector3 terrainPosition;

    void Start()
    {
        terrainData = terrain.terrainData;
        terrainPosition = terrain.transform.position;
        SpawnObjects();
    }

    void SpawnObjects()
    {
        origamiFigures.ForEach((q) =>
        {
            Vector3 spawnPosition = new Vector3();

            do
            {
                spawnPosition = GetRandomTerrainPosition();
            } while (!Physics.CheckSphere(spawnPosition, 1.0f));

            Instantiate(q, spawnPosition, Quaternion.identity);
        });

        for (int i = 0; i < otherRewardsCount; i++)
        {
            GoldPile prefab = otherRewards[Random.Range(0, otherRewards.Count)];
            Vector3 spawnPosition = new Vector3();

            do
            {
                spawnPosition = GetRandomTerrainPosition();
            } while (!Physics.CheckSphere(spawnPosition, 1.0f));

            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
        Instantiate(keyChest, GetRandomTerrainPosition(), Quaternion.identity);
    }

    Vector3 GetRandomTerrainPosition()
    {
        float randomX = Random.Range(terrainPosition.x, terrainPosition.x + terrainData.size.x);
        float randomZ = Random.Range(terrainPosition.z, terrainPosition.z + terrainData.size.z);

        float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + terrainPosition.y;
        return new Vector3(randomX, terrainHeight, randomZ);
    }

    bool CheckSpawnPosition(Vector3 pos)
    {
        return !Physics.CheckSphere(pos, 1.0f);
    }
}
