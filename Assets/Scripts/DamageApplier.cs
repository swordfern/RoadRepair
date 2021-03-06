﻿using UnityEngine;

public class DamageApplier : MonoBehaviour, ITemporarilyDisableable
{
    [SerializeField] private int _damageAmount;
    [SerializeField] private TargetObject _target;
    [SerializeField] private AudioClip _customAudioClip;

    private bool _isPermanentlyDisabled;
    private bool _isTemporarilyDisabled;

    public int DamageAmount => IsDisabled ? 0 : _damageAmount;
    public bool IsDisabled => _isPermanentlyDisabled || _isTemporarilyDisabled;
    public AudioClip CustomAudioClip => _customAudioClip;
    
    private void Awake()
    {
        if (_target != null)
        {
            _target.OnReachedCapacity += HandleTargetReachedCapacity;
        }
    }

    private void HandleTargetReachedCapacity()
    {
        _isPermanentlyDisabled = true;
    }

    public void SetTemporarilyDisabled(bool isDisabled)
    {
        _isTemporarilyDisabled = isDisabled;
    }
}
