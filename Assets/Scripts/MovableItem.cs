using UnityEngine;
using static MovableTargetPairs;

public class MovableItem : MonoBehaviour, IInputTarget
{
    [SerializeField] private MovableType _type;
    private bool _isPlaced;

    public MovableType MovableType => _type;
    public bool CanPlace => !_isPlaced;

    public void Place()
    {
        _isPlaced = true;
    }
}
