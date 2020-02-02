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
    [SerializeField] private bool _showOtherObjectWhenAtCapacity;
    [SerializeField] private GameObject _parentToDisappear;
    [SerializeField] private GameObject _otherObjectToShow;

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
        movableItem.transform.SetParent(_locator, false);

        if (IsAtCapacity())
        {
            HandleReachedCapacity();
            OnReachedCapacity?.Invoke();
        }
        return true;
    }

    private void HandleReachedCapacity()
    {
        if (_showOtherObjectWhenAtCapacity)
        {
            _otherObjectToShow.SetActive(true);
        }

        if (_disappearWhenAtCapacity)
        {
            _parentToDisappear.SetActive(false);
        }
    }

    private bool IsAtCapacity()
    {
        return _placedMovables.Count >= _capacity;
    }
}
