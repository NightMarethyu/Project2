using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IInteractable
{
    private AdventureUIManager uIManager;
    [SerializeField] private string _prompt => "Enter Castle";
    public string InteractionPrompt => _prompt;

    private void Awake()
    {
        uIManager = FindObjectOfType<AdventureUIManager>();
    }

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null) return false;

        if (inventory.HasKey)
        {
            Debug.Log("Entering Castle");
            inventory.HasKey = false;
            return true;
        }

        uIManager.DisplayNotification("You need to find the key");
        return false;
    }
}
