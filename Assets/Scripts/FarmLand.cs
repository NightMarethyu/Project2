using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLand : Building
{
    public override int ResourceGeneration => 10;

    public override string ResourceType => "food";

    public override int buildCost => 25;

    private void Start()
    {
        buildingName = "Farm Land";
    }

    public override void GenerateResources()
    {
        GameManager.Instance.AddResource(ResourceType, ResourceGeneration);
    }

}
