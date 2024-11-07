using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarpentersShopUI : MonoBehaviour
{
    public Button buyMarket;
    public Button buyFarm;
    public Button buyHouse;
    public Button pauseButton;
    public Button unpauseButton;
    public Button saveAndQuitButton;

    public Button returnToMain;

    public GameObject pauseMenu;

    public Building marketplacePrefab;
    public Building farmlandPrefab;
    public Building villageHousePrefab;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI popText;


    void Start()
    {
        buyMarket.onClick.AddListener(() => BuyBuilding(marketplacePrefab));
        buyFarm.onClick.AddListener(() => BuyBuilding(farmlandPrefab));
        buyHouse.onClick.AddListener(() => BuyBuilding(villageHousePrefab));
        pauseButton.onClick.AddListener(ShowPauseMenu);
        unpauseButton.onClick.AddListener(HidePauseMenu);

        saveAndQuitButton.onClick.AddListener(SaveAndQuit);

        returnToMain.onClick.AddListener(ReturnToMain);

        HidePauseMenu();
    }

    void Update()
    {
        goldText.text = "Gold: " + GameManager.Instance.gold;
        foodText.text = "Food: " + GameManager.Instance.food;
        popText.text = "Pop: " + GameManager.Instance.population;
    }

    public void BuyBuilding(Building preFab)
    {
        if (TurnManager.Instance.actionsRemaining > 0 && GameManager.Instance.gold >= preFab.buildCost)
        {
            TurnManager.Instance.UseAction();
            GameManager.Instance.SetBuildingToPlace(preFab);
            GameManager.Instance.AddResource(Constants.Gold, -preFab.buildCost);
            ReturnToMain();
        }
    }

    public void ReturnToMain()
    {
        GameManager.Instance.LoadScene("Main Scene");
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
