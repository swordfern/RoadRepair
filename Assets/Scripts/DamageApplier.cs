using UnityEngine;

public class DamageApplier : MonoBehaviour
{
    [SerializeField] private int _damageAmount;
    [SerializeField] private TargetObject _target;

    private bool _isDisabled;

    public int DamageAmount => _isDisabled ? 0 : _damageAmount;
    public bool IsDisabled => _isDisabled;


    private void Awake()
    {
        if (_target != null)
        {
            _target.OnReachedCapacity += HandleTargetReachedCapacity;
        }
    }

    private void HandleTargetReachedCapacity()
    {
        _isDisabled = true;
    }
}
