using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _textAnimator;
    [SerializeField] private UIFollowsPlayer _textUIPlacer;
    //[SerializeField] protected UIFollowsPlayer _buttonUIPlacer;
    [SerializeField] protected InteractableBase _interactable;

    protected bool _canInteract;

    protected void ShowText()
    {
        _textAnimator.SetBool("State", true);
        _textUIPlacer.SetInFrontOfPlayer();
    }
    protected void HideText()
    {
        _textAnimator.SetBool("State", false);
    }

    protected void OnInputStart()
    {
        if (_canInteract)
        {
            StartInteraction(_interactable);
        }
    }
    protected void OnInputStop()
    {
        StopInteraction(_interactable);
    }

    protected void StartInteraction(InteractableBase interactable)
    {
        interactable.OnInteract();
    }
    protected void StopInteraction(InteractableBase interactable)
    {
        interactable.OnInteractStop();
    }
    protected void EnableCanInteract()
    {
        _canInteract = true;
    }
    protected void DisableCanInteract()
    {
        _canInteract = false;
    }
}
