using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward : MonoBehaviour
{
    public int rewardMin;
    public int rewardMax;

    public int GetReward()
    {
        return Random.Range(rewardMin, rewardMax);
    }
}
