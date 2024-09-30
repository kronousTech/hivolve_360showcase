using System.Collections;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    private AudioSource _source;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        VideosManager.OnRequestAudioPlay.AddListener(PrepareAudio);
        VideosManager.OnRestartEnd.AddListener(StopAudio);
    }
    private void OnDisable()
    {
        VideosManager.OnRequestAudioPlay.RemoveListener(PrepareAudio);
        VideosManager.OnRestartEnd.RemoveListener(StopAudio);
    }
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void PrepareAudio(AudioClip clip)
    {
        _coroutine = StartCoroutine(LoadAudio(clip));
    }
    private IEnumerator LoadAudio(AudioClip clip)
    {
        yield return new WaitForSeconds(3);

        _source.clip = clip;
        _source.Play();
    }
    private void StopAudio()
    {
        StopCoroutine(_coroutine);

        _source.Stop();
    }
}
