using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public TextMeshProUGUI turnsText;
    public Button endTurnButton;

    public TextMeshProUGUI messageText;

    public GameObject messagePanel;

    private bool messageShowing = false;

    private void Start()
    {
        endTurnButton.onClick.AddListener(TurnManager.Instance.EndTurn);
        messagePanel.SetActive(false);
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
}
