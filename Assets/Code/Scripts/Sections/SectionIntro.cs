public class SectionIntro : SectionBase
{
    private void OnEnable()
    {
        VideosManager.OnRestartEnd.AddListener(ShowElements);
        VideosManager.OnRestartEnd.AddListener(EnableCanInteract);

        VideosManager.OnIntroStart.AddListener(HideElements);
        VideosManager.OnIntroStart.AddListener(DisableCanInteract);

        InputInteractor.OnAdvanceInput.AddListener(OnInputStart);
        InputInteractor.OnAdvanceInputStop.AddListener(OnInputStop);
    }
    private void Start()
    {
        ShowElements();
        EnableCanInteract();
    }

    private void ShowElements()
    {
        ShowText();
        _interactable.SetAvailable();
    }
    private void HideElements()
    {
        HideText();
        _interactable.SetUnavailable();
    }
}