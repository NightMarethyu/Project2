using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Guildhall : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Guild Hall");
    }
}

