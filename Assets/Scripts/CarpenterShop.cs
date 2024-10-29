using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpenterShop : MonoBehaviour
{
    [SerializeField] public Building marketplacePrefab;
    [SerializeField] public Building farmlandPrefab;

    public void BuyMarketplace()
    {
        if (GameManager.Instance.gold >= 50)
        {
            Debug.Log("Buying Marketplace");
            GameManager.Instance.SetBuildingToPlace(marketplacePrefab);
            GameManager.Instance.AddResource("gold", -50);
            // GameManager.Instance.LoadScene("MainScene");
        }
    }

    public void BuyFarmLand()
    {
        if (GameManager.Instance.gold >= 25)
        {
            GameManager.Instance.SetBuildingToPlace(farmlandPrefab);
            GameManager.Instance.AddResource("gold", -30);
            // GameManager.Instance.LoadScene("MainScene");
        }
    }

    private void OnMouseDown()
    {
        BuyFarmLand();
    }
}
