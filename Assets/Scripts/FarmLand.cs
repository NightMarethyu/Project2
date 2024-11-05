using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLand : Building
{
    public override int ResourceGeneration => 10;

    public override string ResourceType => Constants.FarmResource;

    public override int buildCost => 25;

    public override string buildingName => Constants.FarmLand;
    protected override int populationNeeded => 2;


}
