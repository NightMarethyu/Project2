using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public TextMeshProUGUI turnString;
    public int turnCount;

    public void EndTurn()
    {
        GameManager.Instance.EndTurn();
    }

    public void UpdateTurnUI()
    {
        turnString.text = "Turns Remaining: " + turnCount;
    }
}
