using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionScriptUI interactionScriptUI;
    private AnimationControls animationControls;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    private void Awake()
    {
        animationControls = GetComponent<AnimationControls>();
    }

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if (_interactable != null)
            {
                if (!interactionScriptUI.isDisplayed)
                    interactionScriptUI.SetUp(_interactable.InteractionPrompt);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    bool wasSuccess = _interactable.Interact(this);
                    if (_interactable is IDestructable)
                        _colliders[0].GetComponent<IDestructable>().Goodbye();
                    animationControls.PlayTargetAnimation("Interact", true);
                    if (_interactable is Castle && wasSuccess)
                        GameManager.Instance.LoadScene("DungeonInterior");
                }
            }
        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (interactionScriptUI.isDisplayed) interactionScriptUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
