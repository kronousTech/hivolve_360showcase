using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VideosManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _transition;
    [Header("Settings")]
    [SerializeField] private VideoInfo[] _videos;

    public static UnityEvent OnExperienceStart = new();
    public static UnityEvent<VideoInfo> OnRequestVideoLoad = new();
    public static UnityEvent OnSectionEndCooldown = new();
    public static UnityEvent OnSectionEnd = new();

    public static UnityEvent OnSectionRestartRequest = new();

    private int _index;

    private Coroutine _videoSection;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartExperience();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RestartBehavior());
        }
    }
#endif

    public void StartExperience()
    {
        _index = 0;

        OnExperienceStart?.Invoke();

        _videoSection = StartCoroutine(VideoBehavior());
    }

    public IEnumerator VideoBehavior()
    {
        _transition.SetTrigger("Transition");

        yield return new WaitForSeconds(1.5f);

        OnRequestVideoLoad?.Invoke(_videos[_index]);

        var sectionDuration = _videos[_index].audioClip.length;

        yield return new WaitForSeconds(sectionDuration);

        OnSectionEndCooldown?.Invoke();

        // COOLDOWN BEFORE NEXT VIDEO

        yield return new WaitForSeconds(5);

        // NEXT VIDEO BEHAVIOR

        OnSectionEnd?.Invoke();

        NextVideo();
    }

    private IEnumerator RestartBehavior()
    {
        Debug.LogWarning("Restarting");

        StopCoroutine(_videoSection);

        _transition.SetTrigger("Restart");

        yield return new WaitForSeconds(1.5f);

        OnSectionRestartRequest?.Invoke();
    }

    public void NextVideo()
    {
        _index++;

        if(_index >= _videos.Length)
        {
            StartCoroutine(RestartBehavior());
        }
        else
        {
            StartCoroutine(VideoBehavior());
        }
    } 
}


[Serializable] 
public struct VideoInfo
{
    public string videoName;
    public AudioClip audioClip;
}
