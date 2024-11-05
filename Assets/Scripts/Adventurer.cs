using UnityEngine;

[CreateAssetMenu(fileName = "New Adventurer", menuName = "Guild/Adventurer")]
public class Adventurer : ScriptableObject
{
    public string adventurerName;
    public int minReward;
    public int maxReward;
    public int hireCost;
    public string flavorText;
    public GameObject adventurerPrefab;
}
