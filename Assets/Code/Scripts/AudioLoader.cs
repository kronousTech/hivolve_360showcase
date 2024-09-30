using System.Collections;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    private AudioSource _source;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        VideosManager.OnTransitionStart.AddListener(StopAudio);
        VideosManager.OnRequestAudioPlay.AddListener(PrepareAudio);
        VideosManager.OnRestartEnd.AddListener(StopAudio);
    }
  
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void PrepareAudio(AudioClip clip)
    {
        StartCoroutine(LoadAudio(clip));
    }
    private IEnumerator LoadAudio(AudioClip clip)
    {
        yield return new WaitForSeconds(4);

        _source.clip = clip;
        _source.Play();
    }
    private void StopAudio()
    {
        StopAllCoroutines();
        //if (_coroutine != null)
        //{
        //    StopCoroutine(_coroutine);
        //}

        _source.Stop();
    }
}
