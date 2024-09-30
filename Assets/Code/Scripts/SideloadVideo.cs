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
            VideosManager.OnRequestVideoPlay.AddListener(PlayVideo);
            VideosManager.OnRestartEnd.AddListener(StopVideo);
        }
        private void OnDisable()
        {
            VideosManager.OnRequestVideoLoad.RemoveListener(LoadVideo);
            VideosManager.OnRequestVideoPlay.RemoveListener(PlayVideo);
            VideosManager.OnRestartEnd.RemoveListener(StopVideo);
        }
        private void Awake()
        {
            _player = GetComponent<VideoPlayer>();
        }

        private void LoadVideo(string info)
        {
#if UNITY_EDITOR
            _player.url = Path.Combine("file://C:/Users/joaos/Downloads/", info);
#else
            _player.url = Path.Combine(Application.persistentDataPath, info);
#endif
            _player.Prepare();
        }

        private void PlayVideo()
        {
            _player.Play();
        }

        private void StopVideo()
        {
            _player.Stop();
        }
    }
}

