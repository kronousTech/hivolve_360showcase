using UnityEngine;
using UnityEngine.Events;

public class InputInteractor : MonoBehaviour
{
    public static UnityEvent OnRestartInput = new();
    public static UnityEvent OnRestartInputStop = new();
    public static UnityEvent OnAdvanceInput = new();
    public static UnityEvent OnAdvanceInputStop = new();
    public static UnityEvent OnRetreatInput = new();
    public static UnityEvent OnRetreatInputStop = new();

    private int _currentInput = -1;

    private void Update()
    {
        // Restart
        if ((OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two))
            || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
        {
            if(_currentInput != 0)
            {
                StopCurrentInput(_currentInput);

                _currentInput = 0;

                OnRestartInput?.Invoke();
            }
        }
        // Advance
        else if (OVRInput.Get(OVRInput.Button.One) || Input.GetKey(KeyCode.D))
        {
            if (_currentInput != 1)
            {
                StopCurrentInput(_currentInput);

                _currentInput = 1;

                OnAdvanceInput?.Invoke();
            }
        }
        // Return
        else if (OVRInput.Get(OVRInput.Button.Two) || Input.GetKey(KeyCode.S))
        {
            if (_currentInput != 2)
            {
                StopCurrentInput(_currentInput);

                _currentInput = 2;

                OnRetreatInput?.Invoke();
            }
        }
        else
        {
            if (_currentInput != -1)
            {
                StopCurrentInput(_currentInput);

                _currentInput = -1;
            }
        }
    }

    private static void StopCurrentInput(int input)
    {
        switch (input)
        {
            case -1: 
                return;
            case 0: OnRestartInputStop?.Invoke();
                break;
            case 1: OnAdvanceInputStop?.Invoke();
                break;
            case 2: OnRetreatInputStop?.Invoke();
                break;
        }
    }
}
