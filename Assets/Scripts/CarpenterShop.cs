using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarpenterShop : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Carpenters Shop");
    }
}
