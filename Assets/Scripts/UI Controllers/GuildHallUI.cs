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
    public Button sendOnControlledQuestButton;
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
        sendOnQuestButton.onClick.AddListener(() => SendOnQuest(true));
        sendOnControlledQuestButton.onClick.AddListener(() => SendOnQuest(false));
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
        rewardsText.text = $"Quest Rewards:\nSimple Quest {currentAdventurer.simpleRewardMin} - {currentAdventurer.simpleRewardMax}\nAdvanced Quest: {currentAdventurer.goalCount} Fortunes";
        costText.text = $"Adventurer Fees\nSimple Quest: {currentAdventurer.hireCostSimple}\nAdvanced Quest: {currentAdventurer.hireCostAdvanced}";
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

    private void SendOnQuest(bool isSimple)
    {
        if (TurnManager.Instance.hasSentAdventurer)
        {
            StartCoroutine(DisplayWarningText(alreadySentAdventurer));
            return;
        }

        Adventurer selectedAdventurer = adventurers[currentAdventurerIndex];

        int curCost = isSimple ? selectedAdventurer.hireCostSimple : selectedAdventurer.hireCostAdvanced;
        // Debug.Log($"Adventurer {selectedAdventurer.adventurerName} is going on a quest!");

        // Check if player has enough gold to hire the adventurer
        if (GameManager.Instance.gold < curCost)
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
        GameManager.Instance.gold -= curCost;
        GameManager.Instance.selectedAdventurer = selectedAdventurer;
        TurnManager.Instance.hasSentAdventurer = true;
        TurnManager.Instance.UseAction();

        // Trigger the "Send On Quest" animation
        currentModel.GetComponent<Animator>().SetTrigger("Send On Quest");

        // Start the coroutine to simulate quest time and reward
        StartCoroutine(CompleteQuestAfterDelay(isSimple));
    }

    private void ReturnToMain()
    {
        GameManager.Instance.LoadScene("Main Scene");
    }

    // Coroutine for quest delay and reward calculation
    private IEnumerator CompleteQuestAfterDelay(bool isSimple)
    {
        // Delay to simulate quest time
        yield return new WaitForSeconds(3f); // Adjust delay as needed

        // Check for simplicity and follow logic
        if (isSimple)
        {
            int rewards = Random.Range(GameManager.Instance.selectedAdventurer.simpleRewardMin, GameManager.Instance.selectedAdventurer.simpleRewardMax + 1);
            GameManager.Instance.message = $"{GameManager.Instance.selectedAdventurer.name} returned successfully. You recieved {rewards} gold!";
            GameManager.Instance.gold += rewards;
            GameManager.Instance.LoadScene("Main Scene");
        } else
        {
            GameManager.Instance.LoadScene("OutdoorAdventure");
        }
    }

    private IEnumerator DisplayWarningText(GameObject warning)
    {
        warning.SetActive(true);

        yield return new WaitForSeconds(3f);

        warning.SetActive(false);
    }

}
