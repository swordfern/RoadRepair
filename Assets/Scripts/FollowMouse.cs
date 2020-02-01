using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zPosition;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;  
    }

    void Update()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _cachedTransform.localPosition = new Vector3(mousePosition.x, mousePosition.y, _zPosition);
    }
}
