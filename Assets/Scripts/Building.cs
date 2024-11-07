using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public virtual string buildingName { get; }
    public virtual int buildCost { get; }
    public virtual int ResourceGeneration { get; }
    public virtual string ResourceType { get; }
    public int populationUsed { get; protected set; }
    protected virtual int populationNeeded { get; }

    public virtual void GenerateResources()
    {
        if (populationUsed == populationNeeded)
        {
            GameManager.Instance.AddResource(ResourceType, ResourceGeneration);
        }
        else
        {
            int populationAvailable = GameManager.Instance.population - GameManager.Instance.populationUsed;
            if (populationAvailable >= populationNeeded)
            {
                GameManager.Instance.populationUsed += populationNeeded;
                populationUsed += populationNeeded;
                GameManager.Instance.AddResource(ResourceType, ResourceGeneration);
            } else if (populationAvailable > 0)
            {
                populationUsed += populationAvailable;
                GameManager.Instance.populationUsed -= populationAvailable;
            }
        }
    }

    public virtual int AdjustPopulationAfterFoodShortage(int remainingPopulation)
    {
        if (populationUsed > remainingPopulation)
        {
            populationUsed = remainingPopulation;
        }
        return populationUsed;
    }

}
