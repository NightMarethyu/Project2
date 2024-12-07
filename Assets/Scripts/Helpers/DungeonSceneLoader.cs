using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSceneLoader : MonoBehaviour
{
    public Adventurer defaultAdventurer;
    public Vector3 spawnPoint;

    private void Awake()
    {
        if (GameManager.Instance.selectedAdventurer != null)
        {
            Instantiate(GameManager.Instance.selectedAdventurer.playableAdventurer, spawnPoint, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No Adventurer selected or PlayerSelectionManager is missing");
            Instantiate(defaultAdventurer.playableAdventurer, spawnPoint, Quaternion.identity);
            GameManager.Instance.selectedAdventurer = defaultAdventurer;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.currentGoalProgress >= GameManager.Instance.selectedAdventurer.goalCount)
        {
            FindObjectOfType<AdventureUIManager>().ReturnToVillage(1);
        }
    }
}
