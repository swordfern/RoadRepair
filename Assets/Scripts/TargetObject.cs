using UnityEngine;
using static MovableTargetPairs;

public class TargetObject : MonoBehaviour
{
    [SerializeField] private TargetType _type;
    public TargetType TargetType => _type;

    public MovableItem _placedMovable;

    public bool HasPlacedItem()
    {
        return _placedMovable != null;
    }

    public void PlaceMovable(MovableItem movableItem)
    {
        _placedMovable = movableItem;
    }
}
