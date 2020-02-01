using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovableTargetPairs", menuName = "MovableTargetPairs", order = 1)]
public class MovableTargetPairs : ScriptableObject
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

    public bool IsMatch(MovableType movable, TargetType target)
    {
        for (int i = 0; i < _pairs.Count; i++)
        {
            var pair = _pairs[i];
            if (pair.Movable == movable && pair.Target == target)
            {
                return true;
            }
        }

        return false;
    }
}
