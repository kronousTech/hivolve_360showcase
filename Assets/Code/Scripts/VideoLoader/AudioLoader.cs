using System.Collections;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    private AudioSource _source;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        VideosManager.OnRequestVideoLoad.AddListener(PrepareAudio);
        VideosManager.OnSectionRestartRequest.AddListener(StopAudio);
    }
    private void OnDisable()
    {
        VideosManager.OnRequestVideoLoad.RemoveListener(PrepareAudio);
        VideosManager.OnSectionRestartRequest.RemoveListener(StopAudio);
    }
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void PrepareAudio(VideoInfo info)
    {
        _coroutine = StartCoroutine(LoadAudio(info));
    }
    private IEnumerator LoadAudio(VideoInfo info)
    {
        yield return new WaitForSeconds(3);

        _source.clip = info.audioClip;
        _source.Play();
    }
    private void StopAudio()
    {
        StopCoroutine(_coroutine);

        _source.Stop();
    }
}
