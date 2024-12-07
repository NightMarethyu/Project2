using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI popText;

    public Button pauseButton;
    public Button saveAndQuitButton;
    public Button resumeGameButton;

    public GameObject pauseMenu;

    void Start()
    {
        pauseButton.onClick.AddListener(ShowPauseMenu);
        resumeGameButton.onClick.AddListener(HidePauseMenu);
        saveAndQuitButton.onClick.AddListener(SaveAndQuit);

        HidePauseMenu();
    }

    void Update()
    {
        goldText.text = "Gold: " + GameManager.Instance.gold;
        foodText.text = "Food: " + GameManager.Instance.food;
        popText.text = "Pop: " + GameManager.Instance.population;
    }

    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        
    }

    private void HidePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        
    }

    private void SaveAndQuit()
    {
        GameManager.Instance.SaveGameState();
        Application.Quit();
    }
}
