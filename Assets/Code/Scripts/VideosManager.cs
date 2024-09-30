using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VideosManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _transition;
    [SerializeField] private GameObject _introBlackground;
    [SerializeField] private Animator _introBlurredBlackground;
    [SerializeField] private GameObject _endBlackground;

    [SerializeField] private UIFollowsPlayer _followEndPanel;
    [SerializeField] private Animator _endPanelAnimator;
    [SerializeField] private AudioClip _endAudio;
 
    [Header("Settings")]
    [SerializeField] private AudioClip _intro;
    [SerializeField] private VideoInfo[] _videos;

    [Header("Debug")]
    [SerializeField] private int _index;

    public static UnityEvent OnIntroStart = new();
    public static UnityEvent OnIntroEnd = new();
    public static UnityEvent OnTransitionStart = new();
    public static UnityEvent OnTransitionEnd = new();
    public static UnityEvent OnVideoEnd = new();
    public static UnityEvent OnRestartStart = new();
    public static UnityEvent OnRestartEnd = new();

    public static UnityEvent OnEndSectionStart = new();

    public static UnityEvent OnNextVideoAvailable = new();
    public static UnityEvent OnPrevVideoAvailable = new();

    public static UnityEvent<AudioClip> OnRequestAudioPlay = new();
    public static UnityEvent<string> OnRequestVideoLoad = new();
    public static UnityEvent OnRequestVideoPlay = new();

    private void Start()
    {
        _introBlackground.SetActive(true);
        _introBlurredBlackground.gameObject.SetActive(true);
        _endBlackground.SetActive(false);
        _endPanelAnimator.SetBool("State", false);
    }

    public void StartIntro()
    {
        StartCoroutine(IntroBehavior());
    }
    public IEnumerator IntroBehavior()
    {
        yield return null;

        OnIntroStart?.Invoke();

        _index = 0;

        _introBlurredBlackground.SetBool("Visible", false);

        // Play Intro Audio
        OnRequestAudioPlay?.Invoke(_intro);

        yield return new WaitForSeconds(_intro.length + 3);

        OnIntroEnd?.Invoke();
    }

    public void StartSections()
    {
        StartCoroutine(VideoBehavior());
    }
    public IEnumerator VideoBehavior()
    {
        yield return null;

        OnTransitionStart?.Invoke();

        _transition.SetTrigger("Transition");

        yield return new WaitForSeconds(1f);

        _introBlackground.SetActive(false);
        _introBlurredBlackground.gameObject.SetActive(false);

        OnRequestVideoLoad?.Invoke(_videos[_index].videoName);

        yield return new WaitForSeconds(0.1f);

        OnTransitionEnd?.Invoke();

        OnRequestVideoPlay?.Invoke();
        OnRequestAudioPlay?.Invoke(_videos[_index].audioClip);

        yield return new WaitForSeconds(1f);

        var sectionDuration = _videos[_index].audioClip.length;

        yield return new WaitForSeconds(sectionDuration + 7);

        OnVideoEnd?.Invoke();

        if (_index + 1 >= _videos.Length)
        {
            StartCoroutine(EndBehavior());

            yield break;
        }
        
        if (_index + 1 < _videos.Length)
        {
            OnNextVideoAvailable?.Invoke();
        }
        if(_index - 1 >= 0)
        {
            OnPrevVideoAvailable?.Invoke();
        }
    }
    private IEnumerator EndBehavior()
    {
        OnEndSectionStart?.Invoke();

        Debug.LogWarning("Ending");

        _transition.SetTrigger("Restart");

        yield return new WaitForSeconds(1f);

        OnRequestAudioPlay?.Invoke(_endAudio);

        _endBlackground.SetActive(true);
        _followEndPanel.SetInFrontOfPlayer();
        _endPanelAnimator.SetBool("State", true);
    }

    private IEnumerator RestartBehavior()
    {
        OnRestartStart?.Invoke();

        Debug.LogWarning("Restarting");

        _transition.SetTrigger("Restart");

        yield return new WaitForSeconds(1f);

        _introBlackground.SetActive(true);
        _introBlurredBlackground.gameObject.SetActive(true);
        _introBlurredBlackground.SetBool("Visible", true);
        _endBlackground.SetActive(false);
        _endPanelAnimator.SetBool("State", false);

        OnRestartEnd?.Invoke();
    }

    public void Restart()
    {
        ClearTransitions();

        StartCoroutine(RestartBehavior());
    }

    public void NextVideo()
    {
        ClearTransitions();

        _index++;

        StartCoroutine(VideoBehavior());
    }
    public void PreviousVideo()
    {
        ClearTransitions();

        if (_index == 0)
        {
            return;
        }

        _index--;

        StartCoroutine(VideoBehavior());
    }

    private void ClearTransitions()
    {
        StopAllCoroutines();

        //if (_videoSection != null)
        //{
        //    StopCoroutine(_videoSection);
        //}
        //if (_introSection != null)
        //{
        //    StopCoroutine(_introSection);
        //}
    }
}


[Serializable] 
public struct VideoInfo
{
    public string videoName;
    public AudioClip audioClip;
}
