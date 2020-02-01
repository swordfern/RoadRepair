using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _velocityPerSecond;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    private void Update()
    {
        var delta = Time.deltaTime;
        var positionDelta = _velocityPerSecond * delta;
        _cachedTransform.localPosition += positionDelta;
    }
}
