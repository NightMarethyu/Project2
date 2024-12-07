using UnityEngine;

[CreateAssetMenu(fileName = "New Adventurer", menuName = "Guild/Adventurer")]
public class Adventurer : ScriptableObject
{
    public string adventurerName;
    public int hireCostSimple;
    public int hireCostAdvanced;
    public int simpleRewardMin;
    public int simpleRewardMax;
    public string flavorText;
    public GameObject adventurerPrefab;
    public GameObject playableAdventurer;
    public Vector3 outdoorSpawnPoint;
    public int goalCount; // the number of origami figures needed to end the adventure with this adventurer
    public int turnCost; // the number of turns it costs to hire this adventurer
}
