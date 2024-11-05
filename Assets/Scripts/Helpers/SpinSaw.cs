using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSaw : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.right, 5);
    }
}
