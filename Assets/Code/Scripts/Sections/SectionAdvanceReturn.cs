using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionAdvanceReturn : SectionBase
{
    [SerializeField] private InteractableBase _returnButton;

    private bool _canInteractPrevious = false;

    private void OnEnable()
    {
        VideosManager.OnTransitionStart.AddListener(HideElements);
        VideosManager.OnRestartStart.AddListener(HideElements);
        VideosManager.OnVideoEnd.AddListener(ShowText);
        VideosManager.OnEndSectionStart.AddListener(HideElements);

        VideosManager.OnNextVideoAvailable.AddListener(ShowNextButton);
        VideosManager.OnPrevVideoAvailable.AddListener(ShowReturnButton);

        InputInteractor.OnAdvanceInput.AddListener(OnInputStart);
        InputInteractor.OnAdvanceInputStop.AddListener(OnInputStop);
        InputInteractor.OnRetreatInput.AddListener(OnInputStartPrevious);
        InputInteractor.OnRetreatInputStop.AddListener(OnInputStopPrevious);

    }
    private void Start()
    {
        HideElements();
    }

    protected void OnInputStartPrevious()
    {
        if (_canInteractPrevious)
        {
            StartInteraction(_returnButton);
        }
    }
    protected void OnInputStopPrevious()
    {
        StopInteraction(_returnButton);
    }

    private void ShowNextButton()
    {
        _interactable.SetAvailable();
        _canInteract = true;
    }
    private void ShowReturnButton()
    {
        _returnButton.SetAvailable();
        _canInteractPrevious = true;
    }

    private void HideElements()
    {
        HideText();
        _interactable.SetUnavailable();
        _returnButton.SetUnavailable();

        _canInteract = false;
        _canInteractPrevious = false;
    }
}