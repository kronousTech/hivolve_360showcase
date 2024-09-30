using UnityEngine;

public class RotateSphereAtStart : MonoBehaviour
{
    private void OnEnable()
    {
        VideosManager.OnTransitionStart.AddListener(RotateForward);
    }
    private void OnDisable()
    {
        VideosManager.OnTransitionStart.RemoveListener(RotateForward);
    }

    private void RotateForward()
    {
        var forward = Camera.main.transform.eulerAngles;
        forward.x = 0;
        forward.y -= 180;
        forward.z = 0;
        transform.eulerAngles = forward;
    }
}
