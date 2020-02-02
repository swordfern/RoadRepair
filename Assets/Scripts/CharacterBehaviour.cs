using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private MovableMover _mover;

    private void Awake()
    {
        _inputController.OnInputTargetFound += HandleInputTargetFound;
    }

    private void HandleInputTargetFound(IInputTarget inputTarget)
    {
        var movable = inputTarget as MovableItem;
        if (movable != null) {
            HandleMovableFound(movable);
        }
        var target = inputTarget as TargetObject;
        if (target != null)
        {
            HandleTargetFound(target);
        }
    }

    private void HandleMovableFound(MovableItem movable)
    {
        _mover.PickeupMovable(movable);
    }

    private void HandleTargetFound(TargetObject target)
    {
        _mover.TryPlaceMovable(target);
    }
}
