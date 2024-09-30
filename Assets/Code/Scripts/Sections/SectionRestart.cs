using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionRestart : SectionBase
{
    [SerializeField] private Animator _buttonAnimator;

    private void OnEnable()
    {
        VideosManager.OnIntroStart.AddListener(EnableButton);

        VideosManager.OnTransitionStart.AddListener(DisableButton);
        VideosManager.OnTransitionEnd.AddListener(EnableButton);

        VideosManager.OnRestartStart.AddListener(DisableButton);
        VideosManager.OnRestartEnd.AddListener(DisableButton);

        InputInteractor.OnRestartInput.AddListener(OnInputStart);
        InputInteractor.OnRestartInputStop.AddListener(OnInputStop);
    }

    private void Start()
    {
        DisableCanInteract();
    }

    private void EnableButton()
    {
        EnableCanInteract();
        ShowElements();
    }
    private void DisableButton()
    {
        DisableCanInteract();
        HideElements();
    }

    private void ShowElements()
    {
        _buttonAnimator.SetBool("State", true);
    }
    private void HideElements()
    {
        _buttonAnimator.SetBool("State", false);
    }
}
