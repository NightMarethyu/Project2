using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public int turnCount;
    public int maxActions;
    public int actionsRemaining;
    public bool hasSentAdventurer;

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

    private void Start()
    {
        actionsRemaining = maxActions;
        hasSentAdventurer = false;
    }

    public void EndTurnCheck()
    {
        if (GameManager.Instance.gold > 50 && actionsRemaining > 0)
        {
            GameManager.Instance.message = "You have actions remaining!";
            return;
        }

        EndTurn();
        
    }

    public void EndTurn() 
    {
        if (turnCount == 0)
        {
            GameManager.Instance.EndGame();
        }

        // End the turn and reset actions
        GameManager.Instance.EndTurn();
        turnCount--;
        actionsRemaining = maxActions;

        if (turnCount % 5 == 0)
        {
            CalculateTaxes();
        }

        // Random event (optional)
        HandleRandomEvent();
        hasSentAdventurer = false;
    }

    public void UseAction()
    {
        if (actionsRemaining > 0)
        {
            actionsRemaining--;
        }
    }

    private void CalculateTaxes()
    {
        int taxAmount = Mathf.FloorToInt(GameManager.Instance.gold * 0.1f);
        GameManager.Instance.gold -= taxAmount;
        if (GameManager.Instance.gold < 0)
        {
            GameManager.Instance.EndGame();
        }
        else
        {
            GameManager.Instance.message = $"You paid {taxAmount} gold in taxes";
        }
    }

    private void HandleRandomEvent()
    {
        // Basic random event example
        int randomEvent = Random.Range(0, 100); // 0-100 scale for event chances

        if (randomEvent < 20)
        {
            // Implementing a random positive event (e.g., finding a gold mine)
            GameManager.Instance.AddResource(Constants.Gold, 20);
            GameManager.Instance.message = "You found a gold mine and gained 20 gold!";
        }
        else if (randomEvent < 40)
        {
            // Implementing a negative event (e.g., a monster attack)
            GameManager.Instance.population -= 1;
            GameManager.Instance.message = "A monster attacked! You lost 1 population.";
        }
        // Continue adding more random events as needed
    }
}
