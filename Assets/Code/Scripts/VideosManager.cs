using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VideosManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _transition;
    [SerializeField] private InteractableBase _introButton;
    [SerializeField] private InteractableBase _startButton;
    [SerializeField] private InteractableBase _nextVideoButton;
    [SerializeField] private InteractableBase _restartButton;
    [SerializeField] private GameObject _introBlackground;
    [SerializeField] private Animator _introBlurredBlackground;

    [SerializeField] private UIFollowsPlayer _introUI;
    [SerializeField] private UIFollowsPlayer _startUI;
    [SerializeField] private UIFollowsPlayer _nextUI;


    [Header("Settings")]
    [SerializeField] private AudioClip _intro;
    [SerializeField] private VideoInfo[] _videos;

    public static UnityEvent OnExperienceStart = new();
    public static UnityEvent<AudioClip> OnRequestAudioPlay = new();
    public static UnityEvent<string> OnRequestVideoLoad = new();
    public static UnityEvent OnRequestVideoPlay = new();
    public static UnityEvent OnSectionEnd = new();

    public static UnityEvent OnSectionRestartRequest = new();

    private int _index;

    private Coroutine _videoSection = null;
    private Coroutine _introSection = null;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartIntro();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RestartBehavior());
        }
    }
#endif
    private void Start()
    {
        _introButton.SetAvailable();
        _startButton.SetUnavailable();
        _nextVideoButton.SetUnavailable();
        _restartButton.SetUnavailable();

        _introBlackground.SetActive(true);
        _introBlurredBlackground.gameObject.SetActive(true);

        _introUI.SetInFrontOfPlayer();
    }


    public void StartIntro()
    {
        _introSection = StartCoroutine(IntroBehavior());
    }
    public IEnumerator IntroBehavior()
    {
        _index = 0;

        _restartButton.SetAvailable();

        _introBlurredBlackground.SetBool("Visible", false);

        // Play Intro Audio
        OnRequestAudioPlay?.Invoke(_intro);

        yield return new WaitForSeconds(_intro.length + 3);

        // Show start experience button
        _startButton.SetAvailable();
        _startUI.SetInFrontOfPlayer();
    }

    public void StartSections()
    {
        OnExperienceStart?.Invoke();

        _videoSection = StartCoroutine(VideoBehavior());
    }

    public IEnumerator VideoBehavior()
    {
        _restartButton.SetUnavailable();
        _nextVideoButton.SetUnavailable();

        _transition.SetTrigger("Transition");

        yield return new WaitForSeconds(1f);

        _introBlackground.SetActive(false);
        _introBlurredBlackground.gameObject.SetActive(false);

        OnRequestVideoLoad?.Invoke(_videos[_index].videoName);

        yield return new WaitForSeconds(1f);

        _restartButton.SetAvailable();
        

        OnRequestVideoPlay?.Invoke();
        OnRequestAudioPlay?.Invoke(_videos[_index].audioClip);

        var sectionDuration = _videos[_index].audioClip.length;

        yield return new WaitForSeconds(sectionDuration + 6);

        OnSectionEnd?.Invoke();

        _index++;

        if (_index >= _videos.Length)
        {
            Restart();
        }
        else
        {
            _nextVideoButton.SetAvailable();
            _nextUI.SetInFrontOfPlayer();
        }
    }

    private IEnumerator RestartBehavior()
    {
        _restartButton.SetUnavailable();
        _startButton.SetUnavailable();
        _nextVideoButton.SetUnavailable();

        Debug.LogWarning("Restarting");

        if(_videoSection != null)
        {
            StopCoroutine(_videoSection);
        }
        if (_introSection != null)
        {
            StopCoroutine(_introSection);
        }

        _transition.SetTrigger("Restart");

        yield return new WaitForSeconds(1.5f);

        _introBlackground.SetActive(true);
        _introBlurredBlackground.gameObject.SetActive(true);
        _introBlurredBlackground.SetBool("Visible", true);

        OnSectionRestartRequest?.Invoke();

        _introButton.SetAvailable();
        _introUI.SetInFrontOfPlayer();
    }

    public void Restart()
    {
        StartCoroutine(RestartBehavior());
    }

    public void NextVideo()
    {
        StartCoroutine(VideoBehavior());
    } 
}


[Serializable] 
public struct VideoInfo
{
    public string videoName;
    public AudioClip audioClip;
}
