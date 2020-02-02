using UnityEngine;

public class MovableMover : MonoBehaviour
{
    [SerializeField] private MovableTargetPairs _movableTargetPairs;

    private MovableItem _heldItem;

    public void PickeupMovable(MovableItem item)
    {
        _heldItem = item;
    }

    public bool TryPlaceMovable(TargetObject targetObject)
    {
        if (_heldItem == null || targetObject.HasPlacedItem())
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
