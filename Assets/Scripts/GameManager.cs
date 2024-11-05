using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int gold;
    public int food;
    public int population;
    public int populationUsed;

    public Building buildingToPlace;
    public List<Building> placedBuildings;
    public List<BuildingData> buildings;
    private int turnCount;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Access to buildingToPlace
    public void SetBuildingToPlace(Building building)
    {
        buildingToPlace = building;
    }

    public Building GetBuildingToPlace()
    {
        return buildingToPlace;
    }

    public void ClearBuildingToPlace()
    {
        buildingToPlace = null;
    }

    public void AddResource(string type, int amount)
    {
        switch (type)
        {
            case Constants.Gold:
                gold += amount;
                break;

            case Constants.Food:
                food += amount;
                break;

            case Constants.Population:
                population += amount;
                break;

            default:
                Debug.LogWarning("type must be either gold, food, or population");
                break;
        }

    }

    public void EndTurn()
    {
        if (placedBuildings == null) return;

        foreach (Building build in placedBuildings)
        {
            build.GenerateResources();
        }

        turnCount++;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void AddBuilding(Building building)
    {
        placedBuildings.Add(building);
        BuildingData newBuilding = new BuildingData(building.buildingName, building.gameObject.transform.position, Quaternion.identity);
        buildings.Add(newBuilding);
    }

    public void ClearBuildings()
    {
        buildings.Clear();
        placedBuildings.Clear();
    }

    [System.Serializable]
    public class BuildingData
    {
        public string buildingType;
        public Vector3 position;
        public Quaternion rotation;

        public BuildingData(string type, Vector3 pos, Quaternion rot)
        {
            buildingType = type;
            position = pos;
            rotation = rot;
        }
    }
}
