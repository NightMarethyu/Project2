using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int gold;
    public int food;

    public Building buildingToPlace;
    public List<Building> placedBuildings;
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
        if (type == "gold")
        {
            gold += amount;
        } else if (type == "food")
        {
            food += amount;
        } else
        {
            Debug.LogWarning("type must be either gold or food");
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
}
