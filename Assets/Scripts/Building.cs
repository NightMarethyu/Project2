using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    protected string buildingName;
    public abstract int buildCost { get; }
    public abstract int ResourceGeneration { get; }
    public abstract string ResourceType { get; }

    public abstract void GenerateResources();

}
