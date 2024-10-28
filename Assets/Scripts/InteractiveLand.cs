using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLand : MonoBehaviour
{
    Color _startColor;
    [SerializeField] Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        _startColor = _renderer.material.color;
        _renderer.material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }
}
