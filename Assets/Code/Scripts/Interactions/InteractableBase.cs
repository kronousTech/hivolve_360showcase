using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    private BoxCollider _collider;

    private static readonly float LoadTime = 3f;

    private bool _isInteracting = false;
    private bool _activated;

    private float _loadingValue;

    public Action OnInteractableActivated;
    public Action<float, float> OnInteracting;

    private void OnEnable()
    {
        VideosManager.OnSectionRestartRequest.AddListener(ClearActivatedState);
    }
    private void OnDisable()
    {
        VideosManager.OnSectionRestartRequest.RemoveListener(ClearActivatedState);
    }
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (_activated)
        {
            return;
        }

        _loadingValue = 
            Mathf.Clamp(_isInteracting ? _loadingValue += Time.deltaTime : _loadingValue -= Time.deltaTime, 0, LoadTime);

        OnInteracting?.Invoke(LoadTime, _loadingValue);

        if (_loadingValue == LoadTime)
        {
            Debug.LogWarning("Activated");
            SetUnavailable();

            OnInteractableActivated?.Invoke();

            OnActivated();
        }
    }

    public void SetAvailable()
    {
        _loadingValue = 0;
        OnInteracting?.Invoke(LoadTime, _loadingValue);
        _collider.enabled = true;
        _animator.SetBool("State", true);
        _activated = false;
    }
    public void SetUnavailable()
    {
        _collider.enabled = false;
        _animator.SetBool("State", false);
        _activated = true;
    }

    private void ClearActivatedState()
    {
        _activated = false;
        _loadingValue = 0;
    }
    public void OnInteract()
    {
        _isInteracting = true;
    }
    public void OnInteractStop()
    {
        _isInteracting = false;
    }

    protected abstract void OnActivated(); 
}
