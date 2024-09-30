using UnityEngine;
using UnityEngine.Events;

public class InputInteractor : MonoBehaviour
{
    public static UnityEvent OnRestartInput = new();
    public static UnityEvent OnAdvanceInput = new();
    public static UnityEvent OnRetreatInput = new();

    private void Update()
    {
        // Restart
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) || Input.GetKeyDown(KeyCode.R))
        {
            OnRestartInput?.Invoke();
        }
        // Advance
        else if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.D))
        {
            OnAdvanceInput?.Invoke();
        }
        // Return
        else if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.S))
        {
           OnRetreatInput?.Invoke();
        }
    }
}
