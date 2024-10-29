using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marketplace : Building
{
    public override int ResourceGeneration => 10;

    public override string ResourceType => "gold";

    public override int buildCost => 50;

    private void Start()
    {
        buildingName = "Marketplace";
    }

    public override void GenerateResources()
    {
        GameManager.Instance.AddResource(ResourceType, ResourceGeneration);
    }
}
