using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneLoader : MonoBehaviour
{
    public GameObject marketplacePrefab;
    public GameObject farmlandPrefab;
    public GameObject villageHousePrefab;

    private void Start()
    {
        LoadBuildings();
    }

    private void LoadBuildings()
    {
        // Create a copy of the buildings list to avoid modification issues during iteration
        List<GameManager.BuildingData> buildingDataList = new List<GameManager.BuildingData>(GameManager.Instance.buildings);
        GameManager.Instance.ClearBuildings();

        foreach (var buildingData in buildingDataList)
        {
            GameObject prefab = GetPrefabByName(buildingData.buildingType);
            if (prefab != null)
            {
                // Find the InteractiveLand object at the specified position
                Collider[] hitColliders = Physics.OverlapSphere(buildingData.position, 0.1f);
                foreach (var hitCollider in hitColliders)
                {
                    InteractiveLand land = hitCollider.GetComponent<InteractiveLand>();
                    if (land != null && !land.isOccupied)
                    {
                        // Instantiate the building and set the tile as occupied
                        Building newBuilding = Instantiate(prefab, buildingData.position, buildingData.rotation).GetComponent<Building>();
                        land.PlaceBuilding(newBuilding);
                        break; // Exit the loop once we place the building
                    }
                }
            }
        }
    }

    private GameObject GetPrefabByName(string name)
    {
        switch (name)
        {
            case Constants.Marketplace:
                return marketplacePrefab;
            case Constants.FarmLand:
                return farmlandPrefab;
            case Constants.House:
                return villageHousePrefab;
            default:
                return null;
        }
    }
}
