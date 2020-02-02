using UnityEngine;

public class CarMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _yVelocity;
    [Header("Lateral Movement")]
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private float _spread;

    private Transform _cachedTransform;
    private float _elapsedTime;
    private bool _gameStarted;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    private void Update()
    {
        if (!_gameStarted)
        {
            return;
        }

        var prevPos = _cachedTransform.localPosition;

        var delta = Time.deltaTime;
        _elapsedTime += delta;
        var yDelta = _yVelocity * delta;
        var y = yDelta + prevPos.y;
        var x = GetX();
        _cachedTransform.localPosition = new Vector3(x, y, prevPos.z);        
    }

    private float GetX()
    {       
        // Psuedo-random wave that starts slow and gets faster
        return _amplitude * Mathf.Sin(_frequency * Mathf.Sin(_spread* Mathf.Sin(_elapsedTime) * _elapsedTime) *_elapsedTime);
    }

    public void SetGameStarted()
    {
        _gameStarted = true;
    }
}
