using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OpeningMenu : MonoBehaviour
{
    public Button startGameButton;
    public Button continueGameButton;
    public Button optionsMenuButton;
    public Button cheatMenuButton;
    public Button mainMenuButton;
    public Button infoPanelButton;
    public Button exitButton;
    public Button deleteSaveDataButton;

    public GameObject mainMenu;
    public GameObject optionsMenuCanvas;
    public GameObject cheatMenuCanvas;
    public GameObject infoPanel;

    public Button beginnerButton;
    public Button advancedButton;
    public Button expertButton;

    public TextMeshProUGUI difficultyDetails;

    public Slider turnSlider;
    public Slider actionSlider;
    public Slider goldSlider;
    public Slider foodSlider;

    public TextMeshProUGUI turnCountText;
    public TextMeshProUGUI actionCountText;
    public TextMeshProUGUI goldCountText;
    public TextMeshProUGUI foodCountText;

    public Button cheatSettingsButton;
    public Button mainMenuCheatButton;

    // Start is called before the first frame update
    void Start()
    {
        startGameButton.onClick.AddListener(LoadMainScene);
        continueGameButton.onClick.AddListener(ContinueGame);
        optionsMenuButton.onClick.AddListener(OpenOptionsMenu);
        cheatMenuButton.onClick.AddListener(OpenCheatMenu);
        infoPanelButton.onClick.AddListener(ShowInfoPanel);
        exitButton.onClick.AddListener(Application.Quit);
        deleteSaveDataButton.onClick.AddListener(DeleteSaveData);

        mainMenuButton.onClick.AddListener(OpenMainMenu);

        beginnerButton.onClick.AddListener(() => SetDifficulty(25, 5, 150, 4, 50));
        advancedButton.onClick.AddListener(() => SetDifficulty(30, 3, 100, 3, 30));
        expertButton.onClick.AddListener(() => SetDifficulty(40, 2, 50, 2, 10));

        cheatSettingsButton.onClick.AddListener(() => SetDifficulty((int)turnSlider.value, (int)actionSlider.value, (int)goldSlider.value, 4, (int)foodSlider.value));
        mainMenuCheatButton.onClick.AddListener(OpenMainMenu);

        mainMenu.gameObject.SetActive(true);
        optionsMenuCanvas.gameObject.SetActive(false);
        cheatMenuCanvas.gameObject.SetActive(false);
        infoPanel.gameObject.SetActive(false);

        continueGameButton.gameObject.SetActive(PlayerPrefs.GetInt("GameSave", 0) == 1);
        deleteSaveDataButton.gameObject.SetActive(PlayerPrefs.GetInt("GameSave", 0) == 1);
    }

    void Update()
    {
        turnCountText.text = turnSlider.value.ToString();
        actionCountText.text = actionSlider.value.ToString();
        goldCountText.text = goldSlider.value.ToString();
        foodCountText.text = foodSlider.value.ToString();
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }

    private void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenuCanvas.SetActive(true);
    }

    private void OpenCheatMenu()
    {
        mainMenu.SetActive(false);
        cheatMenuCanvas.SetActive(true);
    }

    private void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenuCanvas.SetActive(false);
        cheatMenuCanvas.SetActive(false);
    }

    private void SetDifficulty(int turns, int actions, int gold, int population, int food)
    {
        string detailsText = "Details:\n";
        detailsText += $"Turns (how long the game will last): {turns}\n";
        detailsText += $"Actions (how much you can do per turn): {actions}\n";
        detailsText += $"Gold (how much gold you start with): {gold}\n";
        detailsText += $"Population (how many people your village starts with): {population}\n";
        detailsText += $"Food (how much food your village has in stock): {food}";

        difficultyDetails.text = detailsText;
        GameManager.Instance.gold = gold;
        GameManager.Instance.population = population;
        GameManager.Instance.food = food;

        TurnManager.Instance.turnCount = turns;
        TurnManager.Instance.maxActions = actions;
    }

    private void ShowInfoPanel()
    {
        StartCoroutine("PanelDisplay");
    }

    private IEnumerator PanelDisplay()
    {
        infoPanel.SetActive(true);

        yield return new WaitForSeconds(10f);

        infoPanel.SetActive(false);
    }

    private void ContinueGame()
    {
        GameManager.Instance.LoadGameState();
        GameManager.Instance.LoadScene("Main Scene");
    }

    private void DeleteSaveData()
    {
        GameManager.Instance.ClearGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
