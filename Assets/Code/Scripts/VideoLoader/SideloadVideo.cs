using System.IO;
using UnityEngine;
using UnityEngine.Video;

namespace KronosTech.VideoLoader
{
    public class SideloadVideo : MonoBehaviour
    {
        private VideoPlayer _player;

        private void OnEnable()
        {
            VideosManager.OnRequestVideoLoad.AddListener(LoadVideo);
            VideosManager.OnSectionRestartRequest.AddListener(StopVideo);
        }
        private void OnDisable()
        {
            VideosManager.OnRequestVideoLoad.RemoveListener(LoadVideo);
            VideosManager.OnSectionRestartRequest.RemoveListener(StopVideo);
        }
        private void Awake()
        {
            _player = GetComponent<VideoPlayer>();
        }

        private void LoadVideo(VideoInfo info)
        {
#if UNITY_EDITOR
            _player.url = Path.Combine("file://C:/Users/joaos/Downloads/", info.videoName);
#else
            _player.url = Path.Combine(Application.persistentDataPath, info.videoName);
#endif
            _player.Play();
        }

        private void StopVideo()
        {
            _player.Stop();
        }
    }
}

