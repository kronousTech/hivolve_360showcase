public class SectionStart : SectionBase
{
    private void OnEnable()
    {
        VideosManager.OnIntroEnd.AddListener(EnableSection);
        VideosManager.OnTransitionStart.AddListener(DisableSection);
        VideosManager.OnRestartStart.AddListener(DisableSection);

        InputInteractor.OnAdvanceInput.AddListener(ForceInputActivate);

        VideosManager.OnIntroStart.AddListener(EnableCanInteract);
        VideosManager.OnTransitionStart.AddListener(DisableCanInteract);
        VideosManager.OnRestartStart.AddListener(DisableCanInteract);
    }
    private void Start()
    {
        DisableSection();
        DisableCanInteract();
    }

    private void EnableSection()
    {
        ShowText();
        _interactable.SetAvailable();
    }

    private void DisableSection()
    {
        HideText();
        _interactable.SetUnavailable();
    }
}
