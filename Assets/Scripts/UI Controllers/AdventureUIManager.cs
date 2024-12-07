using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdventureUIManager : MonoBehaviour
{
    public GameObject notificationTextDisplay;
    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI questStatus;
    public GameObject pauseMenu;

    public bool isPauseMenuActive = false;

    public Button saveAndQuit;
    public Button returnToVillage;
    public Button resumeGame;

    private void Awake()
    {
        isPauseMenuActive = false;
        notificationTextDisplay.SetActive(false);
        pauseMenu.SetActive(isPauseMenuActive);

        saveAndQuit.onClick.AddListener(SaveAndQuit);
        resumeGame.onClick.AddListener(ResumeGame);
        returnToVillage.onClick.AddListener(() => ReturnToVillage(2, true));

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            isPauseMenuActive = true;
            pauseMenu.SetActive(isPauseMenuActive);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        scoreText.text = $"{GameManager.Instance.goldCount}";
        questStatus.text = $"{GameManager.Instance.currentGoalProgress} / {GameManager.Instance.selectedAdventurer.goalCount}";
    }

    private void OnApplicationFocus(bool focus)
    {
        if (isPauseMenuActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void DisplayNotification(string incomingText)
    {
        notificationText.text = incomingText;

        StartCoroutine("ShowMessage");
    }

    private IEnumerator ShowMessage()
    {

        notificationTextDisplay.SetActive(true);

        yield return new WaitForSeconds(5f);

        notificationTextDisplay.SetActive(false);
    }

    public void ResumeGame()
    {
        isPauseMenuActive = false;
        pauseMenu.SetActive(isPauseMenuActive);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SaveAndQuit()
    {
        GameManager.Instance.SaveGameState();
        Application.Quit();
    }

    public void ReturnToVillage(int rewardCost, bool leftEarly = false)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.goldCount /= rewardCost;
        GameManager.Instance.gold += GameManager.Instance.goldCount;

        if (leftEarly)
        {
            GameManager.Instance.message = $"You left the adventure early and lost some rewards. You got ${GameManager.Instance.goldCount} gold.";
        } else
        {
            StartCoroutine("LetPlayersReadFortune");
            GameManager.Instance.message = $"You returned from the quest successfully. You got ${GameManager.Instance.goldCount} gold.";
        }
        
        GameManager.Instance.goldCount = 0;
        GameManager.Instance.returnedFromAdventure = true;
        GameManager.Instance.LoadScene("Main Scene");
    }

    private IEnumerator LetPlayersReadFortune()
    {
        yield return new WaitForSeconds(3);
    }
}
