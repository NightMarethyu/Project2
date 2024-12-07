using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private AnimationControls animationControls;
    [SerializeField] private string[] attackAnimations;

    private void Awake()
    {
        animationControls = GetComponent<AnimationControls>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            string attackName = attackAnimations[Random.Range(0, 3)];
            animationControls.PlayTargetAnimation(attackName, true);
        }
    }
}
