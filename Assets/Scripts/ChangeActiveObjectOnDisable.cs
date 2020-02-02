using UnityEngine;

public class ChangeActiveObjectOnDisable : MonoBehaviour, ITemporarilyDisableable
{
    [SerializeField] private GameObject _defaultObject;
    [SerializeField] private GameObject _objectActiveOnDisable;

    public void SetTemporarilyDisabled(bool isDisabled)
    {
        _defaultObject.SetActive(!isDisabled);
        _objectActiveOnDisable.SetActive(isDisabled);
    }
}
