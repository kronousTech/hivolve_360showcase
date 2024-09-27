using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    private AudioSource _source;

    private void OnEnable()
    {
        VideosManager.OnRequestVideoLoad.AddListener(LoadAudio);
        VideosManager.OnSectionRestartRequest.AddListener(StopAudio);
    }
    private void OnDisable()
    {
        VideosManager.OnRequestVideoLoad.RemoveListener(LoadAudio);
        VideosManager.OnSectionRestartRequest.RemoveListener(StopAudio);
    }
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void LoadAudio(VideoInfo info)
    {
        _source.clip = info.audioClip;
        _source.Play();
    }
    private void StopAudio()
    {
        _source.Stop();
    }
}
