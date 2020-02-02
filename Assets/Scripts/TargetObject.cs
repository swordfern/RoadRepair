using System.Collections.Generic;
using UnityEngine;
using static MovableTargetPairs;

public class TargetObject : MonoBehaviour, IInputTarget
{
    [SerializeField] private TargetType _type;
    [SerializeField] private int _capacity = 1;

    public TargetType TargetType => _type;

    private List<MovableItem> _placedMovables = new List<MovableItem>();

    public bool IsAtCapacity()
    {
        return _placedMovables.Count >= _capacity;
    }

    public void PlaceMovable(MovableItem movableItem)
    {
        if (!IsAtCapacity())
        {
            _placedMovables.Add(movableItem);
        }
    }
}
