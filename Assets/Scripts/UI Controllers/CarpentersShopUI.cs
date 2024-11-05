using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CarpentersShopUI : MonoBehaviour
{
    private UIDocument menuDocument;
    private Button buyMarket;
    private Button buyFarm;
    private Button buyHouse;

    private Button returnToMain;

    public Building marketplacePrefab;
    public Building farmlandPrefab;
    public Building villageHousePrefab;

    private void Awake()
    {
        menuDocument = GetComponent<UIDocument>();
        buyHouse = menuDocument.rootVisualElement.Q("BuyHouse") as Button;
        buyFarm = menuDocument.rootVisualElement.Q("BuyFarmLand") as Button;
        buyMarket = menuDocument.rootVisualElement.Q("BuyMarketplace") as Button;
        returnToMain = menuDocument.rootVisualElement.Q("ReturnToMain") as Button;

        buyMarket.RegisterCallback<ClickEvent>(BuyMarketplace);
        buyFarm.RegisterCallback<ClickEvent>(BuyFarmLand);
        buyHouse.RegisterCallback<ClickEvent>(BuyHouse);
        returnToMain.RegisterCallback<ClickEvent>(ReturnToMain);
    }

    public void BuyMarketplace(ClickEvent click)
    {
        if (GameManager.Instance.gold >= marketplacePrefab.buildCost)
        {
            Debug.Log("Buying Marketplace");
            GameManager.Instance.SetBuildingToPlace(marketplacePrefab);
            GameManager.Instance.AddResource(Constants.Gold, -marketplacePrefab.buildCost);
            ReturnToMain(click);
        }
    }

    public void BuyFarmLand(ClickEvent click)
    {
        if (GameManager.Instance.gold >= farmlandPrefab.buildCost)
        {
            Debug.Log("Buying Farm Land");
            GameManager.Instance.SetBuildingToPlace(farmlandPrefab);
            GameManager.Instance.AddResource(Constants.Gold, -farmlandPrefab.buildCost);
            ReturnToMain(click);
        }
    }

    public void ReturnToMain(ClickEvent click)
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void BuyHouse(ClickEvent click)
    {
        if (GameManager.Instance.gold >= villageHousePrefab.buildCost)
        {
            Debug.Log("Buying Village House");
            GameManager.Instance.SetBuildingToPlace(villageHousePrefab);
            GameManager.Instance.AddResource(Constants.Gold, -villageHousePrefab.buildCost);
            ReturnToMain(click);
        }
    }
}
