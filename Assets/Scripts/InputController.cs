using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<IInputTarget> OnInputTargetFound;

    [SerializeField] private Camera _camera;
    [SerializeField] private KeyCode _primaryKey;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;   
    }

    private void Update()
    {
        if (Input.GetKeyDown(_primaryKey))
        {
            FindTargets();
        }
    }

    private void FindTargets()
    {
        var raycastHits = Physics2D.RaycastAll(_cachedTransform.localPosition, Vector3.forward);
        for (int i = 0; i < raycastHits.Length; i++)
        {
            HandleRaycastHit(raycastHits[i]);
        }
    }

    private void HandleRaycastHit(RaycastHit2D raycastHit)
    {
        var hitObject = raycastHit.transform.gameObject;
        var inputTargets = hitObject.GetComponents<IInputTarget>();
        for (int i = 0; i < inputTargets.Length; i++)
        {
            var inputTarget = inputTargets[i];
            OnInputTargetFound?.Invoke(inputTarget);
        }
    }
}
