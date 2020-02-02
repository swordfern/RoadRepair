using UnityEngine;
using static MovableTargetPairs;

public class MovableItem : MonoBehaviour
{
    [SerializeField] private MovableType _type;
    public MovableType MovableType => _type;    
}
