using UnityEngine;

public class CameraInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    private InteractableBase _lastInteractable;

    private void Update()
    {
        if(Physics.Raycast(new Ray(transform.position, transform.forward), out var hit, 1000f, _mask))
        {
            if(hit.transform.TryGetComponent<InteractableBase>(out var component))
            {
                if(_lastInteractable == null)
                {
                    _lastInteractable = component;
                    _lastInteractable.OnInteract();
                }
                else if(component != _lastInteractable)
                {
                    _lastInteractable = component;
                    _lastInteractable.OnInteract();
                }
            }
            else
            {
                StopInteraction();
            }
        }
        else
        {
            StopInteraction();
        }
    }

    private void StopInteraction()
    {
        if (_lastInteractable != null)
        {
            _lastInteractable.OnInteractStop();
            _lastInteractable = null;
        }
    }
}
