﻿using UnityEngine;

public class MoveBackAndForth : MonoBehaviour, ITemporarilyDisableable
{
    [SerializeField] private float _xMin;
    [SerializeField] private float _xMax;
    [SerializeField] private float _moveTime;

    private Transform _cachedTransform;
    private float _elapsedTime;
    private float _startX;
    private float _targetX;

    private bool _isDisabled;

    private void Awake()
    {
        _cachedTransform = transform;
        var startPos = _cachedTransform.localPosition;
        _startX = _xMin;
        _targetX = _xMax;
        _cachedTransform.localPosition = new Vector3(_startX, startPos.y, startPos.z);
    }

    private void Update()
    {
        if (_isDisabled)
        {
            return;
        }

        var prevPos = _cachedTransform.localPosition;
        var newElapsedTime = _elapsedTime + Time.deltaTime;
        _elapsedTime = Mathf.Min(newElapsedTime, _moveTime);

        var newX = Mathf.Lerp(_startX, _targetX, _elapsedTime / _moveTime);
        _cachedTransform.localPosition = new Vector3(newX, prevPos.y, prevPos.z);
        if (Mathf.Approximately(_elapsedTime, _moveTime))
        {
            Reset();
        }
    }

    private void Reset()
    {
        var newStart = _targetX;
        var newTarget = _startX;
        _startX = newStart;
        _targetX = newTarget;
        _elapsedTime = 0;
        var prevScale = _cachedTransform.localScale;
        _cachedTransform.localScale = new Vector3(-1 * prevScale.x, prevScale.y, prevScale.z);
    }

    public void SetTemporarilyDisabled(bool isDisabled)
    {
        _isDisabled = isDisabled;
    }
}
