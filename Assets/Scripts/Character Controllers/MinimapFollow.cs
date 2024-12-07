using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y += 100;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
