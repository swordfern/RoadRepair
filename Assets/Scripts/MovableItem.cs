using UnityEngine;
using static MovableTargetPairs;

public class MovableItem : MonoBehaviour, IInputTarget
{
    [SerializeField] private MovableType _type;
    public MovableType MovableType => _type;    
}
