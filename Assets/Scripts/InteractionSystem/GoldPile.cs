using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPile : IDestructable, IInteractable
{
    private AdventureUIManager uIManager;
    public string InteractionPrompt => "Pick Up Gold";
    [SerializeField] private int rewardMin;
    [SerializeField] private int rewardMax;

    public bool Interact(Interactor interactor)
    {
        int rewards = Random.Range(rewardMin, rewardMax);
        GameManager.Instance.goldCount += rewards;
        uIManager.DisplayNotification($"You picked up {rewards} gold");
        return true;
    }

    private void Awake()
    {
        uIManager = FindObjectOfType<AdventureUIManager>();
    }
}
