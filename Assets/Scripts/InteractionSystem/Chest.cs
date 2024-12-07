using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : IDestructable, IInteractable
{
    private AdventureUIManager uIManager;
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private void Awake()
    {
        uIManager = FindObjectOfType<AdventureUIManager>();
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening Chest");
        int rewards = Random.Range(600, 1001);
        GameManager.Instance.goldCount += rewards;
        GameManager.Instance.currentGoalProgress++;
        Debug.Log($"Rewards: {rewards}");
        uIManager.DisplayNotification("You received " + rewards + " gold");
        return true;
    }

}
