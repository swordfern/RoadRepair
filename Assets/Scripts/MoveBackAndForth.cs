using UnityEngine;

public class MoveBackAndForth : MonoBehaviour, ITemporarilyDisableable
{
    [SerializeField] private float _xMin;
    [SerializeField] private float _xMax;
    [SerializeField] private float _moveTime;

    private Transform _cachedTransform;
    private float _initialXScale;
    private float _elapsedTime;
    private float _startX;
    private float _targetX;

    private bool _isDisabled;

    private void Awake()
    {
        _cachedTransform = transform;
        _initialXScale = _cachedTransform.localScale.x;
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

        var prevPos = _cachedTransform.position;
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
        if (!_isDisabled)
        {
            ResetOnReenable();
        }
    }

    private void ResetOnReenable()
    {
        var currentPos = _cachedTransform.position;
        var currentX = currentPos.x;
        var totalDistance = _xMax - _xMin;
        if (currentX > _xMax) // Adjust range so current is max
        {
            _startX = currentX;
            _targetX = currentX - totalDistance;
            _elapsedTime = 0;
            var scale = _cachedTransform.localScale;
            _cachedTransform.localScale = new Vector3(-1 * _initialXScale, scale.y, scale.z);
        }
        else if (currentX < _xMin) // Adjust range so current is min
        {
            _startX = currentX;
            _targetX = currentX + totalDistance;
            _elapsedTime = 0;
            var scale = _cachedTransform.localScale;
            _cachedTransform.localScale = new Vector3(_initialXScale, scale.y, scale.z);
        }
        else // Is still in bounds
        {
            var distanceToTarget = _targetX - currentX;
            var percentToTarget = 1 - (distanceToTarget / (_targetX - _startX));
            _elapsedTime = percentToTarget * _moveTime;
        }
    }
}
