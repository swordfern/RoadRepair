using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] Camera _camera;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;   
    }

    void Update()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _cachedTransform.localPosition = mousePosition;
    }
}
