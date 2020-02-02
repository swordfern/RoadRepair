using System;
using System.Collections.Generic;
using UnityEngine;
using static MovableTargetPairs;

public class TargetObject : MonoBehaviour, IInputTarget
{
    [SerializeField] private TargetType _type;
    [SerializeField] private int _capacity = 1;
    [SerializeField] private Transform _placedMovableLocator;
    [SerializeField] private bool _disappearWhenAtCapacity;
    [SerializeField] private GameObject _parentToDisappear;

    public event Action OnReachedCapacity;

    public TargetType TargetType => _type;

    private List<MovableItem> _placedMovables = new List<MovableItem>();
    private Transform _locator;

    private void Awake()
    {
        _locator = _placedMovableLocator == null ? transform : _placedMovableLocator;
    }

    public bool TryPlaceMovable(MovableItem movableItem)
    {
        if (IsAtCapacity())
        {
            return false;
        }

        _placedMovables.Add(movableItem);
        movableItem.transform.SetParent(_locator);

        if (IsAtCapacity())
        {
            OnReachedCapacity?.Invoke();
            if (_disappearWhenAtCapacity)
            {
                _parentToDisappear.SetActive(false);
            }
        }
        return true;
    }

    private bool IsAtCapacity()
    {
        return _placedMovables.Count >= _capacity;
    }
}
