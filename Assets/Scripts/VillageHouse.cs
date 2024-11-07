using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageHouse : Building
{
    public override int buildCost => 40;
    public override string buildingName => Constants.House;
    public override string ResourceType => Constants.Population;
    private int maxOccupancy = 4;
    public int currentOccupancy { get; private set; }

    public override void GenerateResources()
    {
        if (currentOccupancy < maxOccupancy)
        {
            int moveIn = Random.Range(0, (maxOccupancy - currentOccupancy));
            currentOccupancy += moveIn;
            GameManager.Instance.population += moveIn;
        }
    }

    public override int AdjustPopulationAfterFoodShortage(int remainingPopulation)
    {
        if (currentOccupancy > GameManager.Instance.population - remainingPopulation)
        {
            currentOccupancy = GameManager.Instance.population - remainingPopulation;
        }
        return currentOccupancy;
    }
}
