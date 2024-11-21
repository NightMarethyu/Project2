using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int gold;
    public int food;
    public int maxPopulation;
    public int population;
    public int populationUsed;

    public Building buildingToPlace;
    public List<Building> placedBuildings;
    public List<BuildingData> buildings;
    public List<Building> buildingInventory;

    public string message;

    private const string HighScoreKey = "HighScore";
    private const string GameSaveKey = "GameSave";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        message = null;
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
        GenerateResources();
        ProcessPopulationFoodConsumption();
    }

    public void EndGame()
    {
        float score = CalculateScore();
        SaveScore(score);
        SceneManager.LoadScene("Closing Scene");
    }

    public float CalculateScore()
    {
        if (population > 0)
        {
            return (float)(gold + food) / population;
        }
        return 0;
    }

    private void GenerateResources()
    {
        foreach (Building building in placedBuildings)
        {
            building.GenerateResources();
            int houseCount = 0;
            if (building is VillageHouse)
            {
                houseCount++;
            }
            maxPopulation = houseCount * 4;

            if (population >= maxPopulation)
            {
                population = maxPopulation;
            } else
            {
                population += Random.Range(0, maxPopulation - population);
            }
        }
    }

    private void ProcessPopulationFoodConsumption()
    {
        if (food >= population)
        {
            Debug.Log("There is enough food, everyone is fed");
            food -= population;
        } else
        {
            int populationLoss = population - food;
            Debug.Log($"OH NO! There isn't enough food. {populationLoss} people have died");
            food = 0;
            population -= populationLoss;
            population = Mathf.Max(population, 0);

            if (population == 0)
            {
                EndGame();
            }
        }

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

    [System.Serializable]
    public class BuildingDataWrapper
    {
        public List<GameManager.BuildingData> buildings;

        public BuildingDataWrapper(List<GameManager.BuildingData> buildings)
        {
            this.buildings = buildings;
        }
    }

    public List<float> GetHighScores()
    {
        List<float> highScores = new List<float>();
        for (int i = 0; i < 5; i++)
        {
            float score = PlayerPrefs.GetFloat($"{HighScoreKey}_{i}", 0);
            if (score > 0)
            {
                highScores.Add(score);
            }
        }
        return highScores;
    }

    public void SaveScore(float score)
    {
        List<float> highScores = GetHighScores();
        highScores.Add(score);
        highScores.Sort((a, b) => b.CompareTo(a));

        if (highScores.Count > 5)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetFloat($"{HighScoreKey}_{i}", highScores[i]);
        }

        PlayerPrefs.Save();
    }

    public void SaveGameState()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("Food", food);
        PlayerPrefs.SetInt("Population", population);
        PlayerPrefs.SetInt("PopulationUsed", populationUsed);

        BuildingDataWrapper wrapper = new BuildingDataWrapper(buildings);
        string buildingsJson = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("Buildings", buildingsJson);
        PlayerPrefs.SetInt("TurnCount", TurnManager.Instance.turnCount);

        PlayerPrefs.SetInt(GameSaveKey, 1);
        PlayerPrefs.Save();
    }

    public void LoadGameState()
    {
        if (PlayerPrefs.GetInt(GameSaveKey, 0) == 1)
        {
            gold = PlayerPrefs.GetInt("Gold");
            food = PlayerPrefs.GetInt("Food");
            population = PlayerPrefs.GetInt("Population");

            string buildingsJson = PlayerPrefs.GetString("Buildings", "{}");
            BuildingDataWrapper wrapper = JsonUtility.FromJson<BuildingDataWrapper>(buildingsJson);
            buildings = wrapper.buildings ?? new List<BuildingData>();

            TurnManager.Instance.turnCount = PlayerPrefs.GetInt("TurnCount");
        }
    }

    public void ClearGameState()
    {
        PlayerPrefs.DeleteKey("Gold");
        PlayerPrefs.DeleteKey("Food");
        PlayerPrefs.DeleteKey("Population");
        PlayerPrefs.DeleteKey("PopulationUsed");
        PlayerPrefs.DeleteKey("Buildings");
        PlayerPrefs.DeleteKey("TurnCount");
        PlayerPrefs.DeleteKey(GameSaveKey);
    }
}
