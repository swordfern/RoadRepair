using UnityEngine;

public class MovableMover : MonoBehaviour
{
    [SerializeField] private MovableTargetPairs _movableTargetPairs;

    private Transform _cachedTransform;
    private MovableItem _heldItem;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    public void PickeupMovable(MovableItem item)
    {
        if (_heldItem == null)
        {
            _heldItem = item;
            _heldItem.transform.SetParent(_cachedTransform);
        }
    }

    public bool TryPlaceMovable(TargetObject targetObject)
    {
        if (_heldItem == null || targetObject.IsAtCapacity())
        {
            return false;
        }

        if (_movableTargetPairs.IsMatch(_heldItem.MovableType, targetObject.TargetType))
        {
            targetObject.PlaceMovable(_heldItem);
            _heldItem = null;
            return true;
        }

        return false;
    }
}
