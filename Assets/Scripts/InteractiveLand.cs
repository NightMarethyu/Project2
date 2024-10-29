using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLand : MonoBehaviour
{
    Color _startColor;
    [SerializeField] Renderer _renderer;

    public bool isOccupied;
    Building placedBuilding;

    // Start is called before the first frame update
    void Start()
    {
        _startColor = _renderer.material.color;
    }

    private void OnMouseDown()
    {
        if (isOccupied) return;

        Building buildingToPlace = GameManager.Instance.GetBuildingToPlace();
        if (buildingToPlace != null)
        {
            PlaceBuilding(buildingToPlace);
            GameManager.Instance.ClearBuildingToPlace(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceBuilding(Building building)
    {
        if (isOccupied) return;

        placedBuilding = Instantiate(building, gameObject.transform.position, Quaternion.identity);
        GameManager.Instance.placedBuildings.Add(placedBuilding);
        isOccupied = true;
    }

    public void RemoveBuilding()
    {
        if (placedBuilding == null) return;

        isOccupied = false;
        GameManager.Instance.placedBuildings.Remove(placedBuilding);
        Destroy(placedBuilding);
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }
}
