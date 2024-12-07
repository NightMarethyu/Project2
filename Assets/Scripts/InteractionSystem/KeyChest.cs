using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChest : MonoBehaviour, IInteractable
{
    private AdventureUIManager uIManager;
    public string InteractionPrompt => "Get Key";

    private void Awake()
    {
        uIManager = FindObjectOfType<AdventureUIManager>();
    }

    public bool Interact(Interactor interactor)
    {
        interactor.GetComponent<Inventory>().HasKey = true;
        uIManager.DisplayNotification("You got a key!");

        return true;
    }
}
