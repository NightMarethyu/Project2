using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuildHallUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rewardsText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI flavorText;
    public GameObject warningText;
    public GameObject outOfActionsText;
    public GameObject alreadySentAdventurer;

    public Button leftButton;
    public Button rightButton;
    public Button sendOnQuestButton;
    public Button returnToMain;

    public GameObject AdventurerModelPosition;

    public List<Adventurer> adventurers;
    private int currentAdventurerIndex = 0;
    private GameObject currentModel;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAdventurerDisplay();

        leftButton.onClick.AddListener(() => ChangeAdventurer(-1));
        rightButton.onClick.AddListener(() => ChangeAdventurer(1));
        sendOnQuestButton.onClick.AddListener(SendOnQuest);
        returnToMain.onClick.AddListener(ReturnToMain);
    }

    private void UpdateAdventurerDisplay()
    {
        if (adventurers.Count == 0) return;

        Adventurer currentAdventurer = adventurers[currentAdventurerIndex];

        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        currentModel = Instantiate(currentAdventurer.adventurerPrefab, AdventurerModelPosition.transform.position, AdventurerModelPosition.transform.rotation);

        nameText.text = currentAdventurer.adventurerName;
        rewardsText.text = $"Rewards: {currentAdventurer.minReward} - {currentAdventurer.maxReward}";
        costText.text = $"Cost: {currentAdventurer.hireCost}";
        flavorText.text = "Description: \n" + currentAdventurer.flavorText;
    }

    private void ChangeAdventurer(int direction)
    {
        currentAdventurerIndex += direction;

        if (currentAdventurerIndex < 0)
            currentAdventurerIndex = adventurers.Count - 1;
        else if (currentAdventurerIndex >= adventurers.Count)
            currentAdventurerIndex = 0;

        UpdateAdventurerDisplay();
    }

    private void SendOnQuest()
    {

        if (TurnManager.Instance.hasSentAdventurer)
        {
            StartCoroutine(DisplayWarningText(alreadySentAdventurer));
            return;
        }

        Adventurer selectedAdventurer = adventurers[currentAdventurerIndex];
        // Debug.Log($"Adventurer {selectedAdventurer.adventurerName} is going on a quest!");

        // Check if player has enough gold to hire the adventurer
        if (GameManager.Instance.gold < selectedAdventurer.hireCost)
        {
            Debug.LogWarning("Not enough gold to hire this adventurer!");
            StartCoroutine(DisplayWarningText(warningText));
            return;
        } else if (TurnManager.Instance.actionsRemaining == 0)
        {
            Debug.LogWarning("You're out of actions for this turn");
            StartCoroutine(DisplayWarningText(outOfActionsText));
            return;
        }

        // Deduct hire cost from player's gold
        GameManager.Instance.gold -= selectedAdventurer.hireCost;
        TurnManager.Instance.hasSentAdventurer = true;
        TurnManager.Instance.UseAction();

        // Trigger the "Send On Quest" animation
        currentModel.GetComponent<Animator>().SetTrigger("Send On Quest");

        // Start the coroutine to simulate quest time and reward
        StartCoroutine(CompleteQuestAfterDelay(selectedAdventurer));
    }

    private void ReturnToMain()
    {
        GameManager.Instance.LoadScene("Main Scene");
    }

    // Coroutine for quest delay and reward calculation
    private IEnumerator CompleteQuestAfterDelay(Adventurer adventurer)
    {
        // Delay to simulate quest time
        yield return new WaitForSeconds(3f); // Adjust delay as needed

        // Calculate and add random reward to GameManager's gold
        int reward = Random.Range(adventurer.minReward, adventurer.maxReward + 1);
        GameManager.Instance.AddResource(Constants.Gold, reward);
        GameManager.Instance.message = $"Adventurer {adventurer.adventurerName} returned with {reward} gold!";

        // Load main scene after quest completion
        GameManager.Instance.LoadScene("Main Scene");
    }

    private IEnumerator DisplayWarningText(GameObject warning)
    {
        warning.SetActive(true);

        yield return new WaitForSeconds(3f);

        warning.SetActive(false);
    }

}
