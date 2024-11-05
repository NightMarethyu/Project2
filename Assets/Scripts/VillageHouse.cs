using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageHouse : Building
{
    public override string buildingName => Constants.House;
    public override string ResourceType => Constants.Population;
    private int maxOccupancy = 4;
    private int currentOccupancy;

    public override void GenerateResources()
    {
        if (currentOccupancy > maxOccupancy)
        {
            int moveIn = Random.Range(0, (maxOccupancy - currentOccupancy));
            currentOccupancy += moveIn;
            GameManager.Instance.population += moveIn;
        }
    }
}
