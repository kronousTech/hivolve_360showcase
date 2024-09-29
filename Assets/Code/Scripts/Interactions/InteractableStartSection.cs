using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableStartSection : InteractableBase
{
    public UnityEvent _event;

    protected override void OnActivated()
    {
        _event?.Invoke();
    }
}
