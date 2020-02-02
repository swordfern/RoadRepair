using UnityEngine;

public class MovableMover : MonoBehaviour
{
    [SerializeField] private MovableTargetPairs _movableTargetPairs;
    [SerializeField] private float _timeToDrop;

    private Transform _cachedTransform;
    private MovableItem _heldItem;
    private float _dropTimer;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    private void Update()
    {
        if (_heldItem == null)
        {
            return;
        }
            
        _dropTimer -= Time.deltaTime;
        if (_dropTimer <= 0)
        {
            // Reparent to scene root
            SetHeldItemBehaviorsDisabled(false);
            _heldItem.transform.SetParent(null);
            _heldItem = null;

        }
    }

    public void PickeupMovable(MovableItem item)
    {
        if (_heldItem != null || !item.CanPlace)
        {
            return;
        }

        _heldItem = item;
        _heldItem.transform.SetParent(_cachedTransform);
        SetHeldItemBehaviorsDisabled(true);
        _dropTimer = _timeToDrop;

        //Play audio for picking up item
        GameObject.Find("Main Camera").GetComponent<SoundManager>().pickUpSource.PlayOneShot(GameObject.Find("Main Camera").GetComponent<SoundManager>().pickUpClip, 0.5f);
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
                _heldItem.Place();
                _heldItem = null;

                //Play audio for putting down item
                GameObject.Find("Main Camera").GetComponent<SoundManager>().pickUpSource.PlayOneShot(GameObject.Find("Main Camera").GetComponent<SoundManager>().putDownClip, 0.5f);
            }
        }
    }

    private void SetHeldItemBehaviorsDisabled(bool isDisabled)
    {
        if (_heldItem == null)
        {
            return;
        }

        var temporarilyDisableables = _heldItem.GetComponents<ITemporarilyDisableable>();
        for (int i = 0; i < temporarilyDisableables.Length; i++)
        {
            var disableable = temporarilyDisableables[i];
            disableable.SetTemporarilyDisabled(isDisabled);
        }
    }
}
