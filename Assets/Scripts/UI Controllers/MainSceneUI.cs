using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public TextMeshProUGUI turnsText;

    public TextMeshProUGUI messageText;

    public GameObject messagePanel;

    public GameObject endTurnWarning;

    public Button confirmEarlyTurnEnd;

    private bool messageShowing;

    private void Start()
    {
        messagePanel.SetActive(false);
        endTurnWarning.SetActive(false);
        confirmEarlyTurnEnd.onClick.AddListener(() =>
        {
            messagePanel.SetActive(false);
            TurnManager.Instance.EndTurn();
        });
    }

    void Update()
    {
        turnsText.text = "Turns Remaining: " + TurnManager.Instance.turnCount + "\nActions Left: " + TurnManager.Instance.actionsRemaining;

        if (GameManager.Instance.message != null && !messageShowing)
        {
            messageText.text = GameManager.Instance.message;
            StartCoroutine("ShowMessage");
        }
    }

    private IEnumerator ShowMessage()
    {
        messagePanel.SetActive(true);
        messageShowing = true;

        yield return new WaitForSeconds(5f);

        messagePanel.SetActive(false);
        messageShowing = false;
        GameManager.Instance.message = null;
    }

    public void EndTurnCheck()
    {
        if (GameManager.Instance.gold > 50 && TurnManager.Instance.actionsRemaining > 0)
        {
            endTurnWarning.SetActive(true);
        } else
        {
            TurnManager.Instance.EndTurn();
            endTurnWarning.SetActive(false);
        }
    }

    public void DisableTurnWarning()
    {
        endTurnWarning.SetActive(false);
    }
}
