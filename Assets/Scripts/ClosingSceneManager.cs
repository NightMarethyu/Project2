using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ClosingSceneManager : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoresText;

    public Button mainMenu;

    private void Start()
    {
        mainMenu.onClick.AddListener(LoadMainMenu);
        float currentScore = GameManager.Instance.CalculateScore();
        currentScoreText.text = $"Your Score: {currentScore:F2}";

        DisplayHighScores();
    }

    private void DisplayHighScores()
    {
        List<float> highScores = GameManager.Instance.GetHighScores();
        highScoresText.text = "Top 5 Scores:\n";

        for (int i = 0; i < highScores.Count; i++)
        {
            highScoresText.text += $"{i + 1}. {highScores[i]:F2}\n";
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("Opening Menu");
    }
}
