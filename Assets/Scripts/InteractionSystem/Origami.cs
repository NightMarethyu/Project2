using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Origami : IDestructable, IInteractable
{
    public string InteractionPrompt => "Get Fortune";
    public GameObject soundPlayer;
    private AdventureUIManager uIManager;
    private FortuneAPI fortuneAPI;
    [SerializeField] private int rewardMin;
    [SerializeField] private int rewardMax;

    private void Awake()
    {
        uIManager = FindObjectOfType<AdventureUIManager>();
        fortuneAPI = FindObjectOfType<FortuneAPI>();
    }

    public bool Interact(Interactor interactor)
    {
        int rewards = Random.Range(rewardMin, rewardMax);
        GameManager.Instance.goldCount += rewards;
        DisplayFortune(rewards);
        GameManager.Instance.currentGoalProgress++;
        Instantiate(soundPlayer, transform.position, Quaternion.identity);
        return true;
    }

    private async void DisplayFortune(int rewards) 
    {
        string notification = "";
        var fortuneJson = await fortuneAPI.GetFortune();

        if (fortuneJson != null)
        {
            string fortuneText = fortuneAPI.ExtractFortune(fortuneJson);
            string fortune = fortuneAPI.RemoveFortuneReads(fortuneText);
            notification = $"{fortune}\n";
        }

        notification += $"You received {rewards} gold";

        uIManager.DisplayNotification(notification);
    }
}
