using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button button;

    private List<Button> buttons = new List<Button>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();
        button = _document.rootVisualElement.Q("StartGameButton") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayGameClick);

        buttons = _document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(OnPlayGameClick);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnPlayGameClick(ClickEvent click)
    {
        Debug.Log("Button Clicked");
    }

    private void OnAllButtonsClick(ClickEvent click)
    {
        audioSource.Play();
    }
}
