using System;
using System.Collections.Generic;
using UnityEngine;
using static MovableTargetPairs;
using Random = UnityEngine.Random;

public class TargetObject : MonoBehaviour, IInputTarget
{
    [SerializeField] private TargetType _type;
    [SerializeField] private int _capacity = 1;
    [SerializeField] private Transform[] _parentsForPlacedMovables;
    [SerializeField] private bool _disappearWhenAtCapacity;
    [SerializeField] private bool _showOtherObjectWhenAtCapacity;
    [SerializeField] private GameObject _parentToDisappear;
    [SerializeField] private GameObject _otherObjectToShow;

    public event Action OnReachedCapacity;

    public TargetType TargetType => _type;

    private List<MovableItem> _placedMovables = new List<MovableItem>();
    private Transform[] _movableLocators;

    private void Awake()
    {
        _movableLocators = _parentsForPlacedMovables == null || _parentsForPlacedMovables.Length == 0 
            ? new []{ transform } 
            : _parentsForPlacedMovables;
    }

    public bool TryPlaceMovable(MovableItem movableItem)
    {
        if (IsAtCapacity())
        {
            return false;
        }

        _placedMovables.Add(movableItem);
        PlaceMovable(movableItem);

        if (IsAtCapacity())
        {
            HandleReachedCapacity();
            OnReachedCapacity?.Invoke();
        }
        return true;
    }

    private void PlaceMovable(MovableItem movableItem)
    {
        var slot = Random.Range(0, _movableLocators.Length);
        movableItem.transform.SetParent(_movableLocators[slot], false);
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
