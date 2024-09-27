using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphereAtStart : MonoBehaviour
{
    private void OnEnable()
    {
        VideosManager.OnExperienceStart.AddListener(RotateForward);
    }
    private void OnDisable()
    {
        VideosManager.OnExperienceStart.RemoveListener(RotateForward);
    }

    private void RotateForward()
    {
        transform.forward = -Camera.main.transform.forward;
    }
}
