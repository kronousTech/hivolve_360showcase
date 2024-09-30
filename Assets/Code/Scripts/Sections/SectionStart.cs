using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionStart : SectionBase
{
    private void OnEnable()
    {
        VideosManager.OnIntroEnd.AddListener(EnableSection);
        VideosManager.OnTransitionStart.AddListener(DisableSection);
        VideosManager.OnRestartStart.AddListener(DisableSection);

        InputInteractor.OnAdvanceInput.AddListener(OnInputStart);
        InputInteractor.OnAdvanceInputStop.AddListener(OnInputStop);
    }
    private void Start()
    {
        DisableSection();
    }

    private void EnableSection()
    {
        ShowText();
        _interactable.SetAvailable();
        EnableCanInteract();
    }

    private void DisableSection()
    {
        HideText();
        DisableCanInteract();
        _interactable.SetUnavailable();
    }
}
