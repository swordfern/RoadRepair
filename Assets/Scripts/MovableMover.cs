using UnityEngine;

public class MovableMover : MonoBehaviour
{
    [SerializeField] private MovableTargetPairs _movableTargetPairs;

    private Transform _cachedTransform;
    private MovableItem _heldItem;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    public void PickeupMovable(MovableItem item)
    {
        if (_heldItem == null)
        {
            _heldItem = item;
            _heldItem.transform.SetParent(_cachedTransform);

            //Play audio for picking up item
            //gameObject.GetComponent<SoundManager>().pickUpSource.PlayOneShot(gameObject.GetComponent<SoundManager>().pickUpClip);
            SoundManager.MasterSoundManager.pickUpSource.PlayOneShot(SoundManager.MasterSoundManager.pickUpClip);
        }
    }

    public void PlaceMovable(TargetObject targetObject)
    {
        if (_heldItem == null)
        {
            return;
        }

        if (_movableTargetPairs.IsMatch(_heldItem.MovableType, targetObject.TargetType))
        {
            var placedMovable = targetObject.TryPlaceMovable(_heldItem);
            if (placedMovable)
            {
                _heldItem = null;
            }
        }
    }
}
