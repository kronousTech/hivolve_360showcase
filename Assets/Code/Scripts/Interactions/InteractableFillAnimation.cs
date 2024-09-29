using UnityEngine;
using UnityEngine.UI;

public class InteractableFillAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _fill;

    private InteractableBase _interactable;

    private void OnEnable()
    {
        _interactable.OnInteracting += SetAnimation;
    }
    private void OnDisable()
    {
        _interactable.OnInteracting -= SetAnimation;
    }
    private void Awake()
    {
        _interactable = GetComponent<InteractableBase>();
    }
    private void SetAnimation(float maxValue, float value)
    {
        _fill.fillAmount = value / maxValue;
    }
}
