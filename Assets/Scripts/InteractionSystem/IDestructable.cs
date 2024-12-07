using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDestructable : MonoBehaviour
{
    public void Goodbye()
    {
        Debug.Log("Entered the Goodbye call");
        Destroy(gameObject);
    }
}
