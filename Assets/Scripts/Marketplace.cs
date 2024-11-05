using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marketplace : Building
{
    public override string buildingName => Constants.Marketplace;

    public override int ResourceGeneration => 10;

    public override string ResourceType => Constants.MarketResource;

    public override int buildCost => 50;
    protected override int populationNeeded => 1;
}
