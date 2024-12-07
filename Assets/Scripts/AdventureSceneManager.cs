using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureSceneManager : MonoBehaviour
{
    public Adventurer defaultAdventurer;

    private void Awake()
    {
        if (GameManager.Instance.selectedAdventurer != null)
        {
            Instantiate(GameManager.Instance.selectedAdventurer.playableAdventurer, GameManager.Instance.selectedAdventurer.outdoorSpawnPoint, Quaternion.identity);
        } else
        {
            Debug.LogError("No Adventurer selected or PlayerSelectionManager is missing");
            Instantiate(defaultAdventurer.playableAdventurer, defaultAdventurer.outdoorSpawnPoint, Quaternion.identity);
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
