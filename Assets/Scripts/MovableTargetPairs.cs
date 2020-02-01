using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovableTargetPairs", menuName = "MovableTargetPairs", order = 1)]
public class MovableTargetPairs : ScriptableObject, ISerializationCallbackReceiver
{
    public enum MovableType
    {
        Invalid
    }

    public enum TargetType
    {
        Invalid
    }

    [Serializable]
    private class MovableTargetPair
    {
        public MovableType Movable;
        public TargetType Target;
    }

    [SerializeField] private List<MovableTargetPair> _pairs;

    private readonly Dictionary<MovableType, TargetType> _movableTargetDictionary = new Dictionary<MovableType, TargetType>();

    public bool IsMatch(MovableType movable, TargetType target)
    {
        if (_movableTargetDictionary.TryGetValue(movable, out TargetType foundTarget))
        {
            return target == foundTarget;
        }
        return false;
    }

    public void OnBeforeSerialize()
    {
        _pairs = new List<MovableTargetPair>();

        _pairs.Clear();
        foreach (var kvp in _movableTargetDictionary)
        {
            _pairs.Add(new MovableTargetPair { Movable = kvp.Key, Target = kvp.Value });
        }
    }

    public void OnAfterDeserialize()
    {
        _movableTargetDictionary.Clear();

        for (int i = 0; i < _pairs.Count; i++)
        {
            _movableTargetDictionary.Add(_pairs[i].Movable, _pairs[i].Target);
        }
    }
}
