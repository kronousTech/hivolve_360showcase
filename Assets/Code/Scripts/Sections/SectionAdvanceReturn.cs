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

        VideosManager.OnTransitionEnd.AddListener(SetAdvanceEnable);
        VideosManager.OnTransitionStart.AddListener(DisableAdvance);

        VideosManager.OnNextVideoAvailable.AddListener(ShowNextButton);
        VideosManager.OnPrevVideoAvailable.AddListener(ShowReturnButton);

        InputInteractor.OnAdvanceInput.AddListener(ForceInputActivate);
        InputInteractor.OnRetreatInput.AddListener(OnInputStartPrevious);

    }
    private void Start()
    {
        HideElements();
    }

    protected void OnInputStartPrevious()
    {
        if (_canInteractPrevious)
        {
            _returnButton.ForceActivate();
            _canInteractPrevious = false;
        }
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

    private void SetAdvanceEnable()
    {
        _canInteract = true;
        _canInteractPrevious = true;
    }
    private void DisableAdvance()
    {
        _canInteract = false;
        _canInteractPrevious = false;
    }
}