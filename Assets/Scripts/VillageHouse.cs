using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageHouse : Building
{
    public override int buildCost => 40;
    public override string buildingName => Constants.House;
    public override string ResourceType => Constants.Population;

    public override void GenerateResources()
    {
        return;
    }

}
