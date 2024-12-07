using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionManager : MonoBehaviour
{
    public static PlayerSelectionManager Instance;

    public GameObject selectedAdventurerPrefab; // The prefab of the selected adventurer
    public Vector3 spawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectAdventurer(GameObject adventurerPrefab, Vector3 spawn)
    {
        selectedAdventurerPrefab = adventurerPrefab;
        spawnPoint = spawn;
    }
}
